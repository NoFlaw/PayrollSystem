using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using PayrollSystemDemo.Data.Init;
using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Data
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        void Dispose();
    }
    
    public class PayrollContext : DbContext, IDbContext
    {
        public DbSet<BenefitCost> BenefitCost { get; set; }
        public DbSet<BenefitCostType> BenefitCostType { get; set; }
        public DbSet<Dependent> Dependent { get; set; }
        public DbSet<DependentType> DependentType { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Salary> Salary { get; set; }

        static PayrollContext()
        {
            Database.SetInitializer<PayrollContext>(new PayrollDbInitializer());
        }

        public PayrollContext() : base("name=PayrollContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
