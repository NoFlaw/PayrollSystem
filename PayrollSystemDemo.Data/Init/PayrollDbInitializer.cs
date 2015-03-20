using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Data.Init
{
    public class PayrollDbInitializer : DropCreateDatabaseAlways<PayrollContext>
    {
        protected override void Seed(PayrollContext context)
        {

            //Salary Creation
            var salary = new Salary
            {
                PayFrequency = 26,
                SalaryRate = 2000.00M,
                SalaryYear = 2015,
                DateCreated = DateTime.Now
            };

            context.Salary.Add(salary);
            context.SaveChanges();


            //Discount Creation
            var discounts = new List<Discount>
            {
                new Discount
                {
                    DiscountTitle = "0%",
                    DiscountPercent = 0,
                    DiscountYear = 2015,
                    DateCreated = DateTime.Now
                },
                new Discount
                {
                    DiscountTitle = "10%",
                    DiscountPercent = 10,
                    DiscountYear = 2015,
                    DateCreated = DateTime.Now
                },
                new Discount
                {
                    DiscountTitle = "20%",
                    DiscountPercent = 20,
                    DiscountYear = 2016,
                    DateCreated = DateTime.Now.AddYears(1)
                }

            };
            
            discounts.ForEach( d=> context.Discount.Add(d));
            context.SaveChanges();

            var benefitCostTypes = new List<BenefitCostType>
            {
                new BenefitCostType
                {
                    BenefitCostTypeName = "Employee",
                    BenefitCostAmount = 1000.00M,
                    DateCreated = DateTime.Now,

                },
                new BenefitCostType
                {
                    BenefitCostTypeName = "Dependent",
                    BenefitCostAmount = 500.00M,
                    DateCreated = DateTime.Now,
                }
            };
            
            benefitCostTypes.ForEach(b => context.BenefitCostType.Add(b));
            context.SaveChanges();


            var benefitCosts = new List<BenefitCost>
            {
                new BenefitCost
                {
                    Active = true,

                    BenefitCostType = benefitCostTypes.First(x=>x.BenefitCostTypeId == 1)
                },

                new BenefitCost
                {
                    Active = true,

                    BenefitCostType =  benefitCostTypes.First(x=>x.BenefitCostTypeId == 2)
                }
            };

            benefitCosts.ForEach(b => context.BenefitCost.Add(b));
            context.SaveChanges();
            
            //DependentType Creation
            var dependentTypes = new List<DependentType>
            {
                new DependentType
                {
                    DependentTypeName = "Spouse"
                },
                new DependentType
                {
                    DependentTypeName = "Child"
                },
                new DependentType
                {
                    DependentTypeName = "Other"
                }
            };

            dependentTypes.ForEach(p => context.DependentType.Add(p));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee
                {
                    DateCreated = DateTime.Now,
                    Dependents = null,
                    Discount = discounts.First(x => x.DiscountId == 1),
                    FirstName = "Elphonso",
                    LastName = "Bates",
                    Salary = salary,
                    BenefitCost = benefitCosts.First(x=> x.BenefitCostId == 1)
                },

                new Employee
                {
                    DateCreated = DateTime.Now,
                    Dependents = null,
                    Discount = discounts.First(x => x.DiscountId == 1),
                    FirstName = "John",
                    LastName = "Dewitt",
                    Salary = salary,
                    BenefitCost = benefitCosts.First(x=> x.BenefitCostId == 1)
                }
            };

            employees.ForEach(e => context.Employee.Add(e));
            context.SaveChanges();


            var dependents = new List<Dependent>
            {
                new Dependent
                {
                    FirstName = "Brenton",
                    LastName = "Bates",
                    DateCreated = DateTime.Now,
                    Employee = employees.FirstOrDefault(x => x.EmployeeId == 1),
                    DependentType = dependentTypes.First(x => x.DependentTypeId == 2),
                    Discount = discounts.First(x => x.DiscountId == 1),
                    BenefitCost = benefitCosts.First(x=> x.BenefitCostId == 2)
                },
                new Dependent
                {
                    FirstName = "Susy",
                    LastName = "Singer",
                    DateCreated = DateTime.Now,
                    Employee = employees.FirstOrDefault(x => x.EmployeeId == 2),
                    DependentType = dependentTypes.First(x => x.DependentTypeId == 1),
                    Discount = discounts.First(x => x.DiscountId == 1),
                    BenefitCost = benefitCosts.First(x=> x.BenefitCostId == 2)
                }
            };


            dependents.ForEach(d => context.Dependent.Add(d));
            context.SaveChanges();

            //Adding dependents to seperate employees
            var firstEmployee = employees.First();
            firstEmployee.Dependents.Add(dependents.First());

            var secondEmployee = employees.Last();
            secondEmployee.Dependents.Add(dependents.Last());

            context.SaveChanges();

        }
    }
}
