namespace FFLMeetingRoomBookingApp.Web.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        public string MeetingRoom { get; set; }

        public int NumberOfPeople { get; set; }

        public double MeetingDuration { get; set; }

        public string BookedBy { get; set; }

        public string BookedFor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
