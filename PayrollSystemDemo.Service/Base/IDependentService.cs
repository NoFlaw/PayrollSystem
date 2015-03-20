using System.Collections.Generic;
using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Service.Base
{
    public interface IDependentService : IEntityService<Dependent>
    {
        Dependent GetDependentById(int id);
        IEnumerable<DependentType> GetAllDependentTypes();
    }
}
