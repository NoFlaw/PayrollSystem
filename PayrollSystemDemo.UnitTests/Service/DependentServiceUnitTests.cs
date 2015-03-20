using System;
using NUnit.Framework;
using PayrollSystemDemo.Common;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Service;
using Telerik.JustMock;

namespace PayrollSystemDemo.UnitTests.Service
{
    [TestFixture]
    [Category("DependentService.Unit")]
    public sealed class when_creating_a_depdendent : Specification<DependentService, Dependent>
    {
        private Dependent _dependent;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<DependentService>(x => x.Create(_dependent)).Returns(Arg.IsAny<Dependent>).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Dependent Act()
        {
            _dependent = DataFactory.GetDependent;
            MockingContainer.Instance.Create(_dependent);
            return _dependent;
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_dependent_should_be_saved_to_db()
        {
            MockingContainer.AssertAll();
        }
    }

    [TestFixture]
    [Category("DependentService.Unit")]
    public sealed class when_retrieving_dependent_by_id : Specification<DependentService, Dependent>
    {
        private const int Id = 1;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<DependentService>(x => x.GetDependentById(Id)).Returns(Arg.IsAny<Dependent>).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Dependent Act()
        {
            return MockingContainer.Instance.GetDependentById(Id);
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_dependent_should_be_returned_if_found()
        {
            MockingContainer.AssertAll();
        }
    }

    [TestFixture]
    [Category("DependentService.Unit")]
    public sealed class when_deleting_a_dependent : Specification<DependentService, Dependent>
    {
        private Dependent _dependent;

        /// <summary>
        /// Todo: Arrange (New up instances) 
        /// </summary>
        protected override void Arrange()
        {
            MockingContainer.Arrange<DependentService>(x => x.Delete(_dependent)).OccursOnce();
        }

        /// <summary>
        /// Todo: Act (Call the method)
        /// </summary>
        protected override Dependent Act()
        {
            _dependent = DataFactory.GetDependent;
            MockingContainer.Instance.Delete(_dependent);
            return _dependent;
        }

        /// <summary>
        /// Todo: Assert (Make sure what you expect is true)
        /// </summary>
        [Test]
        public void then_dependent_should_be_deleted()
        {
            MockingContainer.AssertAll();
        }
    }
}
