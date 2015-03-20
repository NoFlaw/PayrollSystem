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
        private readonly IUnitOfWork _unitOfWork;

        public BenefitCostTypeService(IUnitOfWork unitOfWork, IRepository<BenefitCostType> benefitCostTypeRepository)
            : base(unitOfWork, benefitCostTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _benefitCostTypeRepository = benefitCostTypeRepository;
            _benefitCostTypeRepository = _unitOfWork.GetRepository<BenefitCostType>();
        }

        public BenefitCostType GetBenefitCostTypeById(int id)
        {
            try
            {
                return _benefitCostTypeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
