using System;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;
using PayrollSystemDemo.Service.Helpers.Enums;

namespace PayrollSystemDemo.Service
{
    public class BenefitCostService : EntityService<BenefitCost>, IBenefitCostService
    {
        private readonly IRepository<BenefitCost> _benefitCostRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBenefitCostTypeService _benefitCostTypeService;

        public BenefitCostService(IUnitOfWork unitOfWork, IRepository<BenefitCost> benefitCostRepository, IBenefitCostTypeService benefitCostTypeService)
            : base(unitOfWork, benefitCostRepository)
        {
            _unitOfWork = unitOfWork;
            _benefitCostRepository = benefitCostRepository;
            _benefitCostTypeService = benefitCostTypeService;
            _benefitCostRepository = _unitOfWork.GetRepository<BenefitCost>();
        }

        public BenefitCost GetBenefitCostById(int id)
        {
            return _benefitCostRepository.GetById(id);
        }

        public BenefitCostType GetBenefitCostTypeById(int id)
        {
            return _benefitCostTypeService.GetBenefitCostTypeById(id);
        }

        public BenefitCost GetEmployeeBenefitCost()
        {
            return new BenefitCost
            {
                Active = true,
                BenefitCostType = GetBenefitCostTypeById((int)BenefitCostTypeEnum.Employee)
            };
        }

        public BenefitCost GetDependentBenefitCost()
        {
            return new BenefitCost
            {
                Active = true,
                BenefitCostType = GetBenefitCostTypeById((int)BenefitCostTypeEnum.Dependent)
            };
        }
    }
}
