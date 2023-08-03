using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ALaCartes
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
        public ALaCarte ALaCarte { get; set; }
        public void OnGet(int Id, IFormFile? file)
        {
            ALaCarte = _db.ALaCarte.FirstOrDefault(u => u.Id == Id);
        }

        public async Task<IActionResult> OnPost(ALaCarte aLaCarte, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\aLaCartes");
                    var extension = Path.GetExtension(file.FileName);

                    if (ALaCarte.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, ALaCarte.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    aLaCarte.ImageUrl = @"\images\buffets\" + fileName + extension;
                }



                _db.ALaCarte.Update(aLaCarte);
                await _db.SaveChangesAsync();
                //TempData["success"] = "Buffet Item created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
