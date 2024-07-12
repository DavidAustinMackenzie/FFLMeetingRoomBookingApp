using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FFLMeetingRoomBookingApp.Web.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Type { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
