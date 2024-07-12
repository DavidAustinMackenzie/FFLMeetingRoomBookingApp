using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FFLMeetingRoomBookingApp.Web.Models;

namespace FFLMeetingRoomBookingApp.Web.Data
{
    public class FFLMeetingRoomBookingAppWebContext : DbContext
    {
        public FFLMeetingRoomBookingAppWebContext (DbContextOptions<FFLMeetingRoomBookingAppWebContext> options)
            : base(options)
        {
        }

        public DbSet<FFLMeetingRoomBookingApp.Web.Models.User> User { get; set; } = default!;
        public DbSet<FFLMeetingRoomBookingApp.Web.Models.MeetingRoom> MeetingRoom { get; set; } = default!;
        public DbSet<FFLMeetingRoomBookingApp.Web.Models.Booking> Booking { get; set; } = default!;
    }
}
