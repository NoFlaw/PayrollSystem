using System;
using System.Data;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.Service
{
    public class EmployeeService : EntityService<Employee>, IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IRepository<Employee> employeeRepository) 
            : base(unitOfWork, employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _employeeRepository = _unitOfWork.GetRepository<Employee>();
        }

        public Employee GetById(int id)
        {
            try
            {
                return _employeeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException(string.Format("Unable to retrieve the employee by the provided ID: {0}, Error: {1}", id, ex.InnerException));
            }
        }

        public override Employee Create(Employee entity)
        {
            try
            {
                //Some special logic here
                _employeeRepository.Add(entity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new DataException(string.Format("Unable to save the student. Error: {0}", ex.InnerException));
            }

            return entity;
        }
    }

}
