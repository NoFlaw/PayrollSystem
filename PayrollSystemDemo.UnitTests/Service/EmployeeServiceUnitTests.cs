using System;
using NUnit.Framework;
using PayrollSystemDemo.Common;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Service;
using Telerik.JustMock;

namespace PayrollSystemDemo.UnitTests.Service
{
    [TestFixture]
    [Category("EmployeeService.Unit")]
    public sealed class when_creating_a_employee : Specification<EmployeeService, Employee>
    {
        private Employee _employee;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<EmployeeService>(x => x.Create(_employee)).Returns(Arg.IsAny<Employee>).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Employee Act()
        {
            _employee = DataFactory.GetEmployee;
            MockingContainer.Instance.Create(_employee);
            return _employee;
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_employee_should_be_saved_to_db()
        {
            MockingContainer.AssertAll();
        }
    }

    [TestFixture]
    [Category("EmployeeService.Unit")]
    public sealed class when_retrieving_employee_by_id : Specification<EmployeeService, Employee>
    {
        private const int Id =1;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<EmployeeService>(x => x.GetById(Id)).Returns(Arg.IsAny<Employee>).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Employee Act()
        {
            return MockingContainer.Instance.GetById(Id);
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_employee_should_be_returned_if_found()
        {
            MockingContainer.AssertAll();
        }
    }

    [TestFixture]
    [Category("EmployeeService.Unit")]
    public sealed class when_deleting_a_employee : Specification<EmployeeService, Employee>
    {
        private Employee _employee;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<EmployeeService>(x => x.Delete(_employee)).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Employee Act()
        {
            _employee = DataFactory.GetEmployee;
            MockingContainer.Instance.Delete(_employee);
            return _employee;
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_employee_should_be_deleted()
        {
            MockingContainer.AssertAll();
        }
    }
}
