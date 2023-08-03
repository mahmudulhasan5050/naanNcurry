using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NaanNCurry.Data;
using NaanNCurry.Model;
using System.Runtime.Intrinsics.X86;

namespace NaanNCurry.Pages.ScheduleBuffets
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
                _db = db;
        }
        [BindProperty]
        public ScheduleBuffet ScheduleBuffet { get; set; }

        public IActionResult OnGet()
        {

            var buffetItemFromDb = _db.Buffet.Where(u => u.ActiveMenu == "Yes");

            ScheduleBuffet scheduleBuffet = new();
            if (buffetItemFromDb != null)
            {
                IEnumerable<SelectListItem> BuffetList = buffetItemFromDb.Select(
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
            if (ModelState.IsValid) {
                await _db.ScheduleBuffet.AddAsync(ScheduleBuffet);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();  
        }
    }
}
