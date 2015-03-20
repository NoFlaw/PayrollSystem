using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystemDemo.Data.Models
{
    public class DependentType
    {
        [Key]
        [Required]
        public int DependentTypeId { get; set; }
        [DisplayName("Dependent Type")]
        public string DependentTypeName { get; set; }

    }
}
