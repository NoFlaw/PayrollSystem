using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Service.Base
{
    public interface IBenefitCostTypeService : IEntityService<BenefitCostType>
    {
        BenefitCostType GetBenefitCostTypeById(int id);
    }
}
