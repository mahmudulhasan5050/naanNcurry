using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace NaanNCurry.Model
{
    public class ScheduleBuffet
    {
        [Key]
        public int Id { get; set; }
        public DateTime BuffetDate { get; set; }
        [Required]
        public int BuffetId { get; set; }
        [ValidateNever]
        public Buffet Buffet { get; set; }
    }
}
