using Microsoft.EntityFrameworkCore;
using NaanNCurry.Model;

namespace NaanNCurry.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Buffet> Buffet { get; set; }
        public DbSet<ScheduleBuffet> ScheduleBuffet { get; set; }
        public DbSet<ALaCarte> ALaCarte { get; set;}
        public DbSet<Reservation> Reservation { get; set; }
    }
}
