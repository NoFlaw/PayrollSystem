using System;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class BenefitCostTypeService : EntityService<BenefitCostType>, IBenefitCostTypeService
    {
        private readonly IRepository<BenefitCostType> _benefitCostTypeRepository;

        public BenefitCostTypeService(IUnitOfWork unitOfWork, IRepository<BenefitCostType> benefitCostTypeRepository)
            : base(unitOfWork, benefitCostTypeRepository)
        {
            _benefitCostTypeRepository = unitOfWork.GetRepository<BenefitCostType>();
        }

        public BenefitCostType GetBenefitCostTypeById(int id)
        {
            try
            {
                return _benefitCostTypeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(string.Format("Unable to retrieve the provided ID: {0}, Error: {1}", id, ex.InnerException));
            }
        }


    }
}
