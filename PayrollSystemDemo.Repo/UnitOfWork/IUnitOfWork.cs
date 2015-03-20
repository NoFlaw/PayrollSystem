using System;
using PayrollSystemDemo.Repo.Repository;

namespace PayrollSystemDemo.Repo.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Commit();
    }
}
