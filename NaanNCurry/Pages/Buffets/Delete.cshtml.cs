using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;
using System.Net;

namespace NaanNCurry.Pages.Buffets
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
        public Buffet Buffet { get; set; }
        

        public void OnGet(int Id)
        {
            Buffet = _db.Buffet.FirstOrDefault(u=>u.Id == Id);     
           
        }


        public async Task<IActionResult> OnPost()
        {
            var buffetFromDb = _db.Buffet.Find(Buffet.Id);

            //remove Image from folder /images/buffets
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, buffetFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }


            if (buffetFromDb != null)
            {
                _db.Buffet.Remove(buffetFromDb);
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
