using FFLMeetingRoomBookingApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FFLMeetingRoomBookingApp.Web.Controllers
{
    public class BookingsController : Controller
    {
        [HttpGet]
        public IActionResult AddBooking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddBookingViewModel viewModel) 
        {
            return View();
        }
    }
}
