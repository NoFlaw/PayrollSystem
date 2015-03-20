using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystemDemo.Data.Models
{
    public class BenefitCostType
    {
        [Key]
        [Required]
        public int BenefitCostTypeId { get; set; }
        
        public string BenefitCostTypeName { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal BenefitCostAmount { get; set; }

        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [NotMapped]
        [DisplayName("Benefit Cost Amount")]
        public string BenefitCostAmountFormated
        {
            get { return string.Format("{0:C2}", BenefitCostAmount); }
        }
    }
}
