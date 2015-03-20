using System;
using System.Collections.Generic;
using System.Linq;
using PayrollSystemDemo.Data.Models;

namespace PayrollSystemDemo.Common
{
    public static class DataFactory
    {
        public static List<Discount> Discounts = new List<Discount>
        {
            new Discount
            {
                DiscountId = 1,
                DiscountTitle = "0%",
                DiscountPercent = 0,
                DiscountYear = 2015,
                DateCreated = DateTime.Now
            },
            new Discount
            {
                DiscountId = 2,
                DiscountTitle = "10%",
                DiscountPercent = 10,
                DiscountYear = 2015,
                DateCreated = DateTime.Now
            },
            new Discount
            {
                DiscountId = 3,
                DiscountTitle = "20%",
                DiscountPercent = 20,
                DiscountYear = 2016,
                DateCreated = DateTime.Now.AddYears(1)
            }

        };

        public static Salary Salary = new Salary
        {
            SalaryId = 1,
            PayFrequency = 26,
            SalaryRate = 2000.00M,
            SalaryYear = 2015,
            DateCreated = DateTime.Now
        };


        public static List<BenefitCostType> BenefitCostTypes = new List<BenefitCostType>
        {
            new BenefitCostType
            {
                BenefitCostTypeId = 1,
                BenefitCostTypeName = "Employee",
                BenefitCostAmount = 1000.00M,
                DateCreated = DateTime.Now,

            },
            new BenefitCostType
            {
                BenefitCostTypeId = 2,
                BenefitCostTypeName = "Dependent",
                BenefitCostAmount = 500.00M,
                DateCreated = DateTime.Now,
            }
        };
            
          
        public static List<BenefitCost> BenefitCosts  = new List<BenefitCost>
        {
            new BenefitCost
            {
                BenefitCostId = 1,
                Active = true,
                BenefitCostType = BenefitCostTypes.First(x => x.BenefitCostTypeId == 1),
                BenefitCostTypeId = 1
            },

            new BenefitCost
            {
                BenefitCostId = 2,
                Active = true,
                BenefitCostType =  BenefitCostTypes.First(x => x.BenefitCostTypeId == 2),
                BenefitCostTypeId = 1

            }
        };

        public static List<DependentType> DependentTypes  = new List<DependentType>
        {
            new DependentType
            {
                DependentTypeId = 1,
                DependentTypeName = "Spouse"
            },
            new DependentType
            {
                DependentTypeId = 2,
                DependentTypeName = "Child"
            },
            new DependentType
            {
                DependentTypeId = 3,
                DependentTypeName = "Other"
            }
        };


        public static Employee GetEmployee = new Employee
        {
            EmployeeId = 1,
            DateCreated = DateTime.Now,
            Dependents = null,
            Discount = Discounts.First(x => x.DiscountId == 1),
            FirstName = "John",
            LastName = "Doe",
            Salary = Salary,
            BenefitCost = BenefitCosts.First(x => x.BenefitCostId == 1)
        };

        public static List<Dependent> Dependents  = new List<Dependent>
        {
            new Dependent
            {
                DependentId = 1,
                FirstName = "Brenton",
                LastName = "Bates",
                DateCreated = DateTime.Now,
                Employee = GetEmployee,
                DependentType = DependentTypes.First(x => x.DependentTypeId == 2), //Child
                Discount = Discounts.First(x => x.DiscountId == 1),
                BenefitCost = BenefitCosts.First(x=> x.BenefitCostId == 2)
            },
            new Dependent
            {
                DependentId = 2,
                FirstName = "Susy",
                LastName = "Singer",
                DateCreated = DateTime.Now,
                Employee = GetEmployee,
                DependentType = DependentTypes.First(x => x.DependentTypeId == 1), //Spouse
                Discount = Discounts.First(x => x.DiscountId == 1),
                BenefitCost = BenefitCosts.First(x=> x.BenefitCostId == 2)
            }
        };

        public static Dependent GetDependent = Dependents.First();

    }
}
