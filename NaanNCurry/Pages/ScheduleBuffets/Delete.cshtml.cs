using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ScheduleBuffets
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ScheduleBuffet ScheduleBuffet { get; set; }
        public void OnGet(int id)
        {
            ScheduleBuffet = _db.ScheduleBuffet.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var scheduleFromDb = _db.ScheduleBuffet.Find(ScheduleBuffet.Id);
            if (scheduleFromDb != null)
            {
                _db.ScheduleBuffet.Remove(scheduleFromDb);
                await _db.SaveChangesAsync();
         

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
