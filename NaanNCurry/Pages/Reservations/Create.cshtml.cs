using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var reservationByEmailFromDb = _db.Reservation.Where(b=> b.Email == Reservation.Email);

            if (reservationByEmailFromDb != null) {
             foreach(var obj in reservationByEmailFromDb) {
                    if (obj.Email == Reservation.Email && obj.DateTime.ToShortDateString().ToString() == Reservation.DateTime.ToShortDateString().ToString()) {
                        ModelState.AddModelError("name", "can not ssave");
                    
                    }
                
                }
            }            



            if (ModelState.IsValid)
            {
                await _db.Reservation.AddAsync(Reservation);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
