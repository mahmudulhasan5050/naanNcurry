using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace NaanNCurry.Model
{
    public class ALaCarte
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public string ActiveMenu { get; set; }
        public string Price { get; set; }
    }
}
