using FFLMeetingRoomBookingApp.Web.Models.Entities;

namespace FFLMeetingRoomBookingApp.Web.Models
{
    public class AddBookingViewModel
    {
        public DateTime BookingDate { get; set; }

        public string MeetingRoom { get; set; }

        public int NumberOfPeople { get; set; }

        public double MeetingDuration { get; set; }

        public required User BookedBy { get; set; }

        public required User BookedFor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
