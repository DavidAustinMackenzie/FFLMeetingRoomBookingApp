using System.ComponentModel.DataAnnotations.Schema;

namespace FFLMeetingRoomBookingApp.Web.Models.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public string MeetingRoom { get; set; }

        public int NumberOfPeople { get; set; }

        public double MeetingDuration { get; set; }

        public int? BookedById { get; set; }

        public User BookedBy { get; set; }

        public int? BookedForId { get; set; }

        public User BookedFor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<User> Users{ get; set;}
    }
}
