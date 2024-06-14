using FFLMeetingRoomBookingApp.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FFLMeetingRoomBookingApp.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
