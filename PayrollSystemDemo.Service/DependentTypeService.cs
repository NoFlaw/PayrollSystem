using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class DependentTypeService : EntityService<DependentType>, IDependentTypeService
    {
        private readonly IRepository<DependentType> _dependentTypeRepository;

        public DependentTypeService(IUnitOfWork unitOfWork, IRepository<DependentType> dependentTypeRepository)
            : base(unitOfWork, dependentTypeRepository)
        {
            _dependentTypeRepository = unitOfWork.GetRepository<DependentType>();
        }
    }
}
