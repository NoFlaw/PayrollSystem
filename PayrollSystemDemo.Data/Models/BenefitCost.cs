using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystemDemo.Data.Models
{
    public class BenefitCost
    {
        [Key]
        [Required]
        public int BenefitCostId { get; set; }

        [Required]
        public int BenefitCostTypeId { get; set; }

        public bool Active { get; set; }

       
        [ForeignKey("BenefitCostTypeId")]
        public virtual BenefitCostType BenefitCostType { get; set; }
    }
}
