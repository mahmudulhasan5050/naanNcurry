using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ScheduleBuffets
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<ScheduleBuffet> ScheduleBuffets { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        //public IEnumerable<ScheduleBuffet> ScheduleBuffet { get; set; }
        public IActionResult OnGet()
        {
            ScheduleBuffets = _db.ScheduleBuffet.Include(s => s.Buffet);
            return Page();
        }
    }
}
