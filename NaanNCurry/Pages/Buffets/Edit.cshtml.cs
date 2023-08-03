using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;
using System.Net;

namespace NaanNCurry.Pages.Buffets
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Buffet Buffet { get; set; }
        

        public void OnGet(int Id, IFormFile? file)
        {
            Buffet = _db.Buffet.FirstOrDefault(u=>u.Id == Id);     
           
        }


        public async Task<IActionResult> OnPost(Buffet buffet, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\buffets");
                    var extension = Path.GetExtension(file.FileName);

                    if ( Buffet.ImageUrl != null) { 
                    var oldImagePath = Path.Combine(wwwRootPath, Buffet.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    buffet.ImageUrl = @"\images\buffets\" + fileName + extension;
                }



                _db.Buffet.Update(buffet);
                await _db.SaveChangesAsync();
                //TempData["success"] = "Buffet Item created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
