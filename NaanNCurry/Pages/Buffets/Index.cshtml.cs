using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.Buffets
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Buffet> Buffets { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
  
        public void OnGet()
        {
            Buffets = _db.Buffet;
        }
    }
}
