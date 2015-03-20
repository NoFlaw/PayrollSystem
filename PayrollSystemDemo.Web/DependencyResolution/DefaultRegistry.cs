using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PayrollSystemDemo.Data;
using PayrollSystemDemo.Repo.Repository;
using PayrollSystemDemo.Repo.UnitOfWork;
using PayrollSystemDemo.Service;
using PayrollSystemDemo.Service.Base;
using PayrollSystemDemo.Web.Models;
using StructureMap.Web;

namespace PayrollSystemDemo.Web.DependencyResolution {
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.LookForRegistries();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<UnitOfWork>();
            For<IDbContext>().HybridHttpOrThreadLocalScoped().Use<PayrollContext>();
            
            For<DbContext>().HybridHttpOrThreadLocalScoped().Use(() => new ApplicationDbContext());
            For<IAuthenticationManager>().Use(o => HttpContext.Current.GetOwinContext().Authentication);
            For(typeof(IRepository<>)).HybridHttpOrThreadLocalScoped().Use(typeof(Repository<>));
            For<IUserStore<ApplicationUser>>().HybridHttpOrThreadLocalScoped().Use<UserStore<ApplicationUser>>();

            //In-Use Services
            For<IEmployeeService>().HybridHttpOrThreadLocalScoped().Use<EmployeeService>();
            For<IBenefitCostTypeService>().HybridHttpOrThreadLocalScoped().Use<BenefitCostTypeService>();
            For<IBenefitCostService>().HybridHttpOrThreadLocalScoped().Use<BenefitCostService>();
            For<ISalaryService>().HybridHttpOrThreadLocalScoped().Use<SalaryService>();
            For<IDiscountService>().HybridHttpOrThreadLocalScoped().Use<DiscountService>();
            For<IDependentService>().HybridHttpOrThreadLocalScoped().Use<DependentService>();
            For<IDependentTypeService>().HybridHttpOrThreadLocalScoped().Use<DependentTypeService>();

        }

        #endregion
    }
}