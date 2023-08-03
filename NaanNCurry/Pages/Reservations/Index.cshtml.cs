using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Reservation> Reservations { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
     
        public IActionResult OnGet()
        {
            Reservations = _db.Reservation;
            return Page();
        }
    }
}
