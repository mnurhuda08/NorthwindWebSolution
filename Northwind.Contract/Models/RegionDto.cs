using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contract.Models
{
    public class RegionDTO
    {
        [Required(ErrorMessage ="RegionId is Required")]
        public int RegionId { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="Minimum 5 Character")]
        public string? RegionDescription { get; set; }
    }
}
