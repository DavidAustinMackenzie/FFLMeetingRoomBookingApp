namespace FFLMeetingRoomBookingApp.Web.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int MeetingRoomId { get; set; }

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumberOfPeople { get; set; }

        public string Participants { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User? User { get; set; }

        public MeetingRoom? MeetingRoom { get; set; }

    }
}
