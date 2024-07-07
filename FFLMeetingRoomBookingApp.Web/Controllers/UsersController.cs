using FFLMeetingRoomBookingApp.Web.Data;
using FFLMeetingRoomBookingApp.Web.Models;
using FFLMeetingRoomBookingApp.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FFLMeetingRoomBookingApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([Bind("UserId,FirstName,LastName,Email,UserName,Password")] User user)
        {
            if (ModelState.IsValid) 
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("DisplayUsers", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayUsers()
        {
            var users = await dbContext.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await dbContext.Users.FindAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User viewModel) 
        {
            var user = await dbContext.Users.FindAsync(viewModel.UserId);

            if(user is not null) 
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.UserName = viewModel.UserName;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("DisplayUsers", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(User viewModel) 
        {
            var user = await dbContext.Users
                .AsNoTracking()                
                .FirstOrDefaultAsync(x => x.UserId == viewModel.UserId);

            if(user is not null) 
            {
                dbContext.Users.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("DisplayUsers", "Users");
        }
    }
}
