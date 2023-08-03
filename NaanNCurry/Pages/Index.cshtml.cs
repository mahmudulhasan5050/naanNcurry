using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NaanNCurry.Data;
using NaanNCurry.Model;
using NaanNCurry.Services.EmailService;

namespace NaanNCurry.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IEmailService _emailService;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db, IEmailService emailService)
        {
            _logger = logger;
            _db = db;
            _emailService = emailService;
        }

        public IEnumerable<ScheduleBuffet> ScheduleBuffets { get; set; }
        public IEnumerable<ALaCarte> ALaCartes { get; set; }
        public EmailDataTransferObject EmailDataTransferObject { get; set; }
        [BindProperty]
        public Reservation Reservation { get; set; }
        public IActionResult OnGet()
        {
            ScheduleBuffets = _db.ScheduleBuffet.Include(s => s.Buffet);
            ALaCartes = _db.ALaCarte;

            ViewData["ScheduleBuffets"] = ScheduleBuffets;
            ViewData["ALaCartes"] = ALaCartes;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var reservationByEmailFromDb = _db.Reservation.Where(b => b.Email == Reservation.Email);

            if (reservationByEmailFromDb != null)
            {
                foreach (var obj in reservationByEmailFromDb)
                {
                    if (obj.Email == Reservation.Email && obj.DateTime.ToShortDateString().ToString() == Reservation.DateTime.ToShortDateString().ToString())
                    {
                        ModelState.AddModelError("name", "can not ssave");

                    }

                }
            }
            EmailDataTransferObject = new()
            {
                To = Reservation.Email,
                Subject = "Confirmation of Booking",
                Body = "<h1>hello, This is the confirmation.</h1>"
            };
          

            if (ModelState.IsValid)
            {
                await _db.Reservation.AddAsync(Reservation);
                await _db.SaveChangesAsync();
                _emailService.SendEmail(EmailDataTransferObject);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}