using System;
using System.Linq;
using NUnit.Framework;
using PayrollSystemDemo.Common;
using PayrollSystemDemo.Data;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service;
using PayrollSystemDemo.Service.Base;

namespace PayrollSystemDemo.IntegrationTests.Service
{
    /// <summary>
    /// Employee Service Retrieving Employee By Id Integration Test
    /// </summary>
    [TestFixture]
    [Category("EmployeeServiceTests.Integration")]
    public class when_retrieving_employee_by_id_using_employee_service
    {
        //Todo: Declare variables needed
        private Employee _employee;
        private IUnitOfWork _unitOfWork;
        private IEmployeeService _employeeService;
        private IRepository<Employee> _employeeRepository;

        [TestFixtureSetUp]
        public void Setup()
        {
            //Todo: Uncomment when using Automapper library
            //AutoMapperConfiguration.Configure();

            //Todo: New up objects and call your services and/or create repos and set private variables
            var context = new PayrollContext();
            _unitOfWork = new UnitOfWork(context);
            _employeeRepository = _unitOfWork.GetRepository<Employee>();
            _employeeService = new EmployeeService(_unitOfWork, _employeeRepository);
            _employee = _employeeService.GetById(1);
        }


        [Test]
        public void then_employee_information_should_be_found()
        {
            Assert.IsNotNull(_employee);
            Assert.AreEqual(_employee.EmployeeId, 1, "Employee.EmployeeId was not the same");
            Assert.AreEqual(_employee.FirstName, "Elphonso", "Employee.FirstName was not the same");
            Assert.AreEqual(_employee.LastName, "Bates", "Employee.LastName was not the same");
            //CodeFirst + the way DateCreated is set, makes this hard to match up unless date was provided manually for test data
            //Assert.AreEqual(_employee.DateCreated, DateTime.Now, "Employee.DateCreated was not the same");

        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            //Todo: Undo your changes (ie: delete records saved in the database, created from Integration Test)
            if (_employee == null || _employee.EmployeeId == 0) return;

            _employeeRepository.Delete(_employee);
            _employeeRepository.Save();
        }
    }

    /// <summary>
    /// Employee Service Saving Student Integration Test
    /// </summary>
    [TestFixture]
    [Category("EmployeeServiceTests.Integration")]
    public class when_saving_employee_using_employee_service
    {
        //Todo: Declare variables needed
        private Employee _employee;
        private IUnitOfWork _unitOfWork;
        private IEmployeeService _employeeService;
        private IRepository<Employee> _employeeRepository;
        private bool _employeeFound;

        [TestFixtureSetUp]
        public void Setup()
        {
            //Todo: Uncomment when using Automapper library
            //AutoMapperConfiguration.Configure();

            //Todo: New up objects and call your services and/or create repos and set private variables
            var context = new PayrollContext();
            _unitOfWork = new UnitOfWork(context);
            _employeeRepository = _unitOfWork.GetRepository<Employee>();
            _employeeService = new EmployeeService(_unitOfWork, _employeeRepository);
            _employee = DataFactory.GetEmployee;
            _employeeService.Create(_employee);
            _employeeFound = _employeeRepository.GetQuery().Any(x => x.EmployeeId == _employee.EmployeeId);

        }


        [Test]
        public void then_employee_information_should_be_saved_to_db()
        {
            //Todo: Check did test pass
            Assert.IsNotNull(_employee, "When creating the student failed because the instance is still null");
            Assert.True(_employee.EmployeeId != 0, "When checking the Student.Id, it still equals 0 and should NOT");
            Assert.IsTrue(_employeeFound, "When trying to find the Student by Student.Id, the student was NOT found");
            Assert.AreEqual(_employee.FirstName, DataFactory.GetEmployee.FirstName, "Student First Name was NOT the same as previously set with DataFactory");
            Assert.AreEqual(_employee.LastName, DataFactory.GetEmployee.LastName, "Student Last Name was NOT the same as previously set with DataFactory");
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            //Todo: Undo your changes (ie: delete records saved in the database, created from Integration Test)
            if (_employee == null || _employee.EmployeeId == 0) return;

            _employeeRepository.Delete(_employee);
            _employeeRepository.Save();
        }
    }
}
