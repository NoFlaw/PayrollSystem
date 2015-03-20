using System.Collections.Generic;

namespace PayrollSystemDemo.Service.Base
{
    public interface IEntityService<T> : IService where T : class
    {
        T Create(T entity);
        bool Delete(T entity);
        IEnumerable<T> GetAll();
        T Update(T entity);
    }
}
