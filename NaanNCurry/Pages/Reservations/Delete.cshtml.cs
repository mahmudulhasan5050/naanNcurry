using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }
        public void OnGet(int id)
        {
            Reservation = _db.Reservation.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var reservationFromDb = _db.Reservation.Find(Reservation.Id);
            if (reservationFromDb != null)
            {
                _db.Reservation.Remove(reservationFromDb);
                await _db.SaveChangesAsync();


                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
