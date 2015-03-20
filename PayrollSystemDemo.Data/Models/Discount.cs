using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystemDemo.Data.Models
{
    public class Discount
    {
        [Key]
        [Required]
        public int DiscountId { get; set; }

        [DisplayName("Discount")]
        public string DiscountTitle { get; set; }

        public int DiscountPercent { get; set; }

        public int DiscountYear { get; set; }
        
        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

    }
}
