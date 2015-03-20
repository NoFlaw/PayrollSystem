using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Service.Base
{
    public interface IBenefitCostService : IEntityService<BenefitCost>
    {
        BenefitCost GetBenefitCostById(int id);
        BenefitCostType GetBenefitCostTypeById(int id);
        BenefitCost GetEmployeeBenefitCost();
        BenefitCost GetDependentBenefitCost();
    }
}
