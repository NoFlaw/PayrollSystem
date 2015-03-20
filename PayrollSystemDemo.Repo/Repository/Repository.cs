using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PayrollSystemDemo.Data;

namespace PayrollSystemDemo.Repo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly IDbContext Context;
        internal readonly IDbSet<T> DbSet;

        public Repository(IDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public void Update(T entity)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public IQueryable<T> GetQuery()
        {
            return DbSet.AsQueryable();
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().AsEnumerable();
        }

        public T Single(Expression<Func<T, bool>> where = null)
        {
            return (where == null)
                       ? DbSet.SingleOrDefault()
                       : DbSet.SingleOrDefault(where);
        }

        public T First(Expression<Func<T, bool>> where = null)
        {
            return (where == null)
                       ? DbSet.First()
                       : DbSet.First(where);
        }

        public int Count
        {
            get { return DbSet.Count(); }
        }

        public int GetCountFor(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate == null)
                       ? DbSet.Count()
                       : DbSet.Count(predicate);
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public bool Exist(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate == null) ? DbSet.Any() : DbSet.Any(predicate);
        }

        public IQueryable<T> GetObjectGraph(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
                query = query.Where(filter);


            if (!String.IsNullOrWhiteSpace(includeProperties))
                query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).AsQueryable() : query.AsQueryable();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate == null) ? DbSet : DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        {
            var skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<T, bool>> predicate = null)
        {
            return (predicate != null) && DbSet.Count(predicate) > 0;
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        /// <summary>
        /// Find by expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Delete by entity type
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);

            DbSet.Remove(entity);
        }

        /// <summary>
        /// Delete by given expression
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entitiesToDelete = Filter(predicate);
            foreach (var entity in entitiesToDelete)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Provides included lookup values 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties == null) return DbSet.AsQueryable();

            foreach (var include in includeProperties)
                DbSet.Include(include);

            return DbSet.AsQueryable();
        }

        /// <summary>
        ///     Adds specified entity to the collection.
        ///     No changes are persisted to the database until the Save is called.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Attaches the entity passed by parameter
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public virtual RepositoryQuery<T> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<T>(this);

            return repositoryGetFluentHelper;
        }

        /// <summary>
        /// Get method via the Query method and return the RepositoryQuery object to provide a fluent “ish” api, 
        /// so that’s its a bit easier and intuitive for developers when querying the Repository layer. 
        /// Note, only methods in our RepositoryQuery will actually invoke the internal Get method, 
        /// again, which is why its marked internal. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        internal IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                                    Func<IQueryable<T>,
                                    IOrderedQueryable<T>> orderBy = null,
                                    List<Expression<Func<T, object>>>
                                    includeProperties = null,
                                    int? page = null,
                                    int? pageSize = null)
        {
            IQueryable<T> query = DbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);


            return query.ToList();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
