using System.Collections.Generic;
using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Web.Models
{
    public class EmployeeDependentViewModel
    {
        public Employee Employee { get; set; }
        public Dependent Dependent { get; set; }
        public ICollection<Dependent> Dependents { get; set; }
    }

}
