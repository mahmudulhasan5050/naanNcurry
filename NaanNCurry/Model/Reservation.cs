using System.ComponentModel.DataAnnotations;

namespace NaanNCurry.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Date & Time")]
        public DateTime DateTime { get; set; }
        [Required]
        [Range(1,20)]
        public int People { get; set; }
        [Display(Name ="Special Request")]
        public string SpecialRequest { get; set; }
    }
}
