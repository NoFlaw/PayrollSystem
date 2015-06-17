using System;
using System.Collections.Generic;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class DependentService : EntityService<Dependent>, IDependentService
    {
        private readonly IRepository<Dependent> _dependentRepository;
        private readonly IDependentTypeService _dependentTypeService;

        public DependentService(IUnitOfWork unitOfWork, IRepository<Dependent> dependentRepository, IDependentTypeService dependentTypeService)
            : base(unitOfWork, dependentRepository)
        {
            _dependentTypeService = dependentTypeService;
            _dependentRepository = unitOfWork.GetRepository<Dependent>();
        }

        public Dependent GetDependentById(int id)
        {
            try
            {
                return _dependentRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(string.Format("Unable to retrieve the dependent by the provided ID: {0}, Error: {1}", id, ex.InnerException));
            }
            
        }

        public IEnumerable<DependentType> GetAllDependentTypes()
        {
            return _dependentTypeService.GetAll();
        }
    }
}
