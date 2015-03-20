using Telerik.JustMock.AutoMock;

namespace PayrollSystemDemo.UnitTests
{
    public abstract class Specification<TTarget, TResult> : SpecificationBase<TTarget> where TTarget : class
    {
        public MockingContainer<TTarget> MockingContainer;

        protected Specification()
        {
            MockingContainer = new MockingContainer<TTarget>();
            Initialize();
        }

        protected sealed override void OnAct()
        {
            Result = Act();
        }

        protected TResult Result { get; private set; }
        protected abstract TResult Act();
    }

    public abstract class Specification<TTarget> : SpecificationBase<TTarget> where TTarget : class
    {
        public MockingContainer<TTarget> MockingContainer;
        protected abstract void Act();

        protected Specification()
        {
            MockingContainer = new MockingContainer<TTarget>();
            Initialize();
        }

        protected sealed override void OnAct()
        {
            Act();
        }  
    }

    public abstract class SpecificationBase<TTarget> :
    SpecificationBase
    {
        protected TTarget Target { get; set; }

        protected SpecificationBase()
        {
            Target = default(TTarget);
        }
    }

    public abstract class SpecificationBase
    {
        public void Initialize()
        {
            //Todo: Uncomment when using Automapper library
            //AutoMapperConfiguration.Configure();

            Arrange();
            OnAct();
        }

        /// <summary>
        /// Arrange (Create the objects and prepare the test)
        /// </summary>
        protected abstract void Arrange();

        /// <summary>
        /// Act (Call the method)
        /// </summary>
        protected abstract void OnAct();
    }
}
