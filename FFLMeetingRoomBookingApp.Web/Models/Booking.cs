namespace FFLMeetingRoomBookingApp.Web.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int MeetingRoomId { get; set; }

        public int UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public int NumberOfPeople { get; set; }

        public double MeetingDuration { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }

        public MeetingRoom MeetingRoom { get; set; }

    }
}
