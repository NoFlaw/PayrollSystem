using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystemDemo.Repo.Repository
{
    public sealed class RepositoryQuery<T> where T : class
    {
        private readonly List<Expression<Func<T, object>>> _includeProperties;
        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderByQuerable;
        private readonly Repository<T> _repository;
        private Expression<Func<T, bool>> _filter;
        private int? _pageSize;
        private int? _page;

        public RepositoryQuery(Repository<T> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<T, object>>>();
        }

        public RepositoryQuery<T> Filter(Expression<Func<T, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public RepositoryQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<T> Include(Expression<Func<T, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<T> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public IEnumerable<T> Get()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }
    }
}
