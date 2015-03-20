using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystemDemo.Data.Models
{
    public class Salary
    {
        [Key]
        [Required]
        public int SalaryId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C2}")]
        public decimal SalaryRate { get; set; }

        public int PayFrequency { get; set; }
        public int SalaryYear { get; set; }

        [DisplayName("Created")]
        public DateTime DateCreated { get; set; }

        [NotMapped]
        [DisplayName("Salary")]
        public string SalaryFormated
        {
            get { return string.Format("{0:C2}", SalaryRate); }
        }
    }
}
