using System;
using System.Collections.Generic;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    /// <summary>
    /// Inheriting from EntityService provides CRUD operations encapsulating Unit Of Work.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        protected EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual T Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Add(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Update(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public virtual bool Delete(T entity)
        {       
            try
            {
                _repository.Delete(entity);
                _unitOfWork.Commit();
                return true;

            }
            catch (Exception ex)
            {
                //Log error with elmah
                var message = ex.Message;
            }
            
            return false;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
