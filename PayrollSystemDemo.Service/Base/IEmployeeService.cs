using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Service.Base
{
    public interface IEmployeeService : IEntityService<Employee>
    {
        Employee GetById(int id);
    }
}

