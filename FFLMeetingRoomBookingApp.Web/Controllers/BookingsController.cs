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
            PopulateUsersDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking([Bind("BookingId,BookingDate,MeetingRoom,NumberOfPeople,MeetingDuration,BookedById,BookedForId")] Booking booking) 
        {
            if (ModelState.IsValid)
            {
                await dbContext.Bookings.AddAsync(booking);
                await dbContext.SaveChangesAsync();
            }

            PopulateUsersDropDownList(booking.BookedForId);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisplayBookings() 
        {
            var bookings = await dbContext.Bookings.ToListAsync();

            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id) 
        {
            var booking = await dbContext.Bookings.FindAsync(id);

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBooking(int? BookingId)
        {
            return RedirectToAction("DisplayBookings", "Bookings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBooking(int? BookingId)
        {
            if(BookingId == null)
            {
                return NotFound();
            }

            var booking = await dbContext.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingId == BookingId);

            if(booking is not null) 
            {
                dbContext.Bookings.Remove(booking);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("DisplayBookings", "Bookings");
        }

        private void PopulateUsersDropDownList(object selectedUser = null)
        {
            var usersQuery = from u in dbContext.Users
                             orderby u.Name
                             select u;
            ViewBag.Users = new SelectList(usersQuery.AsNoTracking(), "UserId", "Name", selectedUser);
        }
    }
}
