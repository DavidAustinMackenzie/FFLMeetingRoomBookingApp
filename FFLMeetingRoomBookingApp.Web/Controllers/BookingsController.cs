using FFLMeetingRoomBookingApp.Web.Data;
using FFLMeetingRoomBookingApp.Web.Models;
using FFLMeetingRoomBookingApp.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> AddBooking()
        {
            //var users = await dbContext.Users.ToListAsync();
            //ViewBag.Users = users; // Pass users to ViewBag or use a view model

            var usersQuery = from u in dbContext.Users
                                   orderby u.FirstName
                                   select u;
            ViewBag.Users = new SelectList(usersQuery.AsNoTracking(), "UserId", "FirstName");
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };



            await dbContext.Bookings.AddAsync(booking);
            await dbContext.SaveChangesAsync();

            return View();
        }
    }
}
