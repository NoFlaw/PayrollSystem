using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystemDemo.Data;
using PayrollSystemDemo.Repo.Repository;

namespace PayrollSystemDemo.Repo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _disposed = false;
        }

        public UnitOfWork()
        {
            _context = new PayrollContext();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            // Checks if the Dictionary Key contains the Model class
            if (_repositories.Keys.Contains((typeof(T))))
            {
                // Return the repository for that Model class
                return _repositories[typeof(T)] as IRepository<T>;
            }

            // If the repository for that Model class doesn't exist, create it
            var repository = new Repository<T>(_context);

            // Add it to the dictionary
            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
