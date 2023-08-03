using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ALaCartes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DeleteModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public ALaCarte ALaCarte { get; set; }

        public IFormFile? file { get; set; }
        public void OnGet(int Id)
        {
            ALaCarte = _db.ALaCarte.FirstOrDefault(u => u.Id == Id);
        }

        public async Task<IActionResult> OnPost()
        {
            var aLaCarteFromDb = _db.ALaCarte.Find(ALaCarte.Id);


            //remove Image from folder /images/aLaCarte
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, aLaCarteFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
      

            if (aLaCarteFromDb != null)
            {
                _db.ALaCarte.Remove(aLaCarteFromDb);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
