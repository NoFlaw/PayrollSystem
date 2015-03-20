using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PayrollSystemDemo.Repo.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get using RepositoryQuery PagedList.Mvc
        /// </summary>
        /// <returns></returns>
        RepositoryQuery<T> Query();

        /// <summary>
        ///   Get all objects from database AsQueryable
        /// </summary>
        IQueryable<T> GetQuery();

        /// <summary>
        ///   Get all objects from database as a List
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        ///   Returns SingleOrDefault that satisfies the where clause
        /// </summary>
        /// <param name="where"> takes an nullable condition </param>
        T Single(Expression<Func<T, bool>> where);

        /// <summary>
        ///   Returns First that satisfies the where clause
        /// </summary>
        /// <param name="where"> takes an nullable condition </param>
        T First(Expression<Func<T, bool>> where);

        /// <summary>
        ///   Adds object to the context underlying set, inserted after SaveChanges() is called
        /// </summary>
        /// <param name="entity"> Specified the object to save. </param>
        void Add(T entity);

        /// <summary>
        ///   Update object changes and save to database.
        /// </summary>
        /// <param name="entity"> Specified the object to save. </param>
        void Update(T entity);

        /// <summary>
        ///   Adds object to the context underlying set as if it was read from the database.
        /// </summary>
        /// <param name="entity"> Specified the object to save. </param>
        void Attach(T entity);

        /// <summary>
        ///   Save to the database.
        /// </summary>
        void Save();

        /// <summary>
        ///   Get the total object count.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///   Get the total count for object that satisfies the where clause
        /// </summary>
        /// <param name="predicate"> takes an nullable condition </param>
        int GetCountFor(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Gets object by primary key.
        /// </summary>
        /// <param name="id"> primary key </param>
        /// <returns> </returns>
        T GetById(object id);

        /// <summary>
        ///   Gets objects via optional filter, sort order, and includes
        /// </summary>
        /// <param name="filter"> </param>
        /// <param name="orderBy"> </param>
        /// <param name="includeProperties"> </param>
        /// <returns> </returns>
        IQueryable<T> GetObjectGraph(Expression<Func<T, bool>> filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     string includeProperties = "");

        /// <summary>
        ///   Gets objects from database by a given filter.
        /// </summary>
        /// <param name="predicate"> Specified a filter </param>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="filter"> Specified a filter </param>
        /// <param name="total"> Returns the total records count of the filter. </param>
        /// <param name="index"> Specified the page index, default: 0 </param>
        /// <param name="size"> Specified the page size, default: 50 </param>
        IQueryable<T> Filter(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50);

        /// <summary>
        ///   Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate"> Specified the filter expression </param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Find object by keys.
        /// </summary>
        /// <param name="keys"> Specified the search keys. </param>
        T Find(params object[] keys);

        /// <summary>
        ///   Find object by specified expression.
        /// </summary>
        /// <param name="predicate"> </param>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Find object by specified expression and within accordance to predicate if provided.
        /// </summary>
        /// <param name="predicate"> </param>
        bool Exist(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Deletes the object by primary key
        /// </summary>
        /// <param name="id"> </param>
        void Delete(object id);

        /// <summary>
        ///   Delete the object from database.
        /// </summary>
        /// <param name="entity"> Specified a existing object to delete. </param>
        void Delete(T entity);

        /// <summary>
        ///   Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"> </param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///   Including all related objects related to given expression
        /// </summary>
        /// <param name="includeProperties"> </param>
        IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}
