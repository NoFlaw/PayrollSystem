using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystemDemo.Data.Models
{
    public class Employee
    {
        [Key]
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int SalaryId { get; set; }   
        
        [Required]
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        public int? DependentTypeId { get; set; }
        public int? DiscountId { get; set; }
        public int? BenefitCostId { get; set; }

        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }

        [ForeignKey("BenefitCostId")]
        public virtual BenefitCost BenefitCost { get; set; }

        [ForeignKey("SalaryId")]
        public virtual Salary Salary { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }

        [NotMapped]
        [DisplayName("Employee")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [NotMapped]
        [DisplayName("Family Benefit Costs")]
        public string FamilyBenefitsCosts
        {

            get
            {
                var amount = Dependents.Count * 500;
                return string.Format("{0:C2}", amount);
            }
        }

        [NotMapped]
        [DisplayName("Total Costs Preview")]
        public string TotalCostsPreview
        {

            get
            {
                var percentage = 0.00M;

                if (Discount.DiscountPercent > 0)
                    percentage += Discount.DiscountPercent;

                foreach (var dependent in Dependents)
                {
                    if (dependent.Discount.DiscountPercent > 0)
                    percentage += dependent.Discount.DiscountPercent;
                }

                var totalCosts = (BenefitCost.BenefitCostType.BenefitCostAmount + (decimal) (Dependents.Count * 500.00));
                var deductionAmount = totalCosts * percentage / 100;
                var totalAmount = totalCosts - deductionAmount;

                return string.Format("{0:C2}", totalAmount);
            }
        }


    }
}
