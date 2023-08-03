using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NaanNCurry.Data;
using NaanNCurry.Model;

namespace NaanNCurry.Pages.ALaCartes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public ALaCarte ALaCarte { get; set; }

        public void OnGet()
        {
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

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    aLaCarte.ImageUrl = @"\images\aLaCartes\" + fileName + extension;
                }



                await _db.ALaCarte.AddAsync(aLaCarte);
                await _db.SaveChangesAsync();
                //TempData["success"] = "Buffet Item created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
