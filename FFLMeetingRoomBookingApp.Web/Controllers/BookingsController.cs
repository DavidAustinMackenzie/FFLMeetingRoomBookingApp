using FFLMeetingRoomBookingApp.Web.Data;
using FFLMeetingRoomBookingApp.Web.Models;
using FFLMeetingRoomBookingApp.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FFLMeetingRoomBookingApp.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BookingsController(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddBooking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddBookingViewModel viewModel) 
        {
            var booking = new Booking 
            {
                BookingDate = viewModel.BookingDate,
                MeetingRoom = viewModel.MeetingRoom,
                NumberOfPeople = viewModel.NumberOfPeople,
                MeetingDuration = viewModel.MeetingDuration,
                BookedBy = viewModel.BookedBy,
                BookedFor = viewModel.BookedFor,
                CreatedAt = viewModel.CreatedAt,
                UpdatedAt = viewModel.UpdatedAt,
            };

            await dbContext.Bookings.AddAsync(booking);
            await dbContext.SaveChangesAsync();

            return View();
        }
    }
}
