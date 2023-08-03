using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ScheduleBuffets
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ScheduleBuffet ScheduleBuffet { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) { 
             return NotFound();
            }

            ScheduleBuffet = await _db.ScheduleBuffet.Include(s => s.Buffet).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (ScheduleBuffet == null) {
                return NotFound();
            }

            var ssss = _db.Buffet;

            if (ssss != null)
            {
                IEnumerable<SelectListItem> BuffetList = ssss.Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString(),
                    }
                    ).ToList();
                ViewData["BuffetList"] = BuffetList;
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()                    
        {
            if (ModelState.IsValid)
            {                          
               _db.ScheduleBuffet.Update(ScheduleBuffet);
                await _db.SaveChangesAsync();
              
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
