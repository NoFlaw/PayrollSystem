using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystemDemo.Data.Models
{
    public class Dependent
    {
        [Key]
        [Required]
        public int DependentId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int DependentTypeId { get; set; }

        [Required]
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        public int? BenefitCostId { get; set; }

        public int? DiscountId { get; set; }
        

        
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }
        
        [ForeignKey("BenefitCostId")]
        public virtual BenefitCost BenefitCost { get; set; }

        [ForeignKey("DependentTypeId")]
        public virtual DependentType DependentType { get; set; }
        
    }
}
