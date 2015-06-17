using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class SalaryService : EntityService<Salary>, ISalaryService
    {
        private readonly IRepository<Salary> _salaryRepository;

        public SalaryService(IUnitOfWork unitOfWork, IRepository<Salary> salaryRepository)
            : base(unitOfWork, salaryRepository)
        {
            _salaryRepository = unitOfWork.GetRepository<Salary>();
        }
    }
}
