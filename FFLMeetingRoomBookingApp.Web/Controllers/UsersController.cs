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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(int? UserId) 
        {
            if (UserId == null)
            {
                return NotFound();
            }

            var userToUpdate = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                u => u.FirstName, 
                u => u.LastName, 
                u => u.Email,
                u => u.UserName,
                u => u.Password))
                {
                    try
                    {
                        await dbContext.SaveChangesAsync();
                        
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }

            return RedirectToAction("DisplayUsers", "Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int? UserId) 
        {
            if (UserId == null)
            {
                return NotFound();
            }

            var user = await dbContext.Users
                .AsNoTracking()                
                .FirstOrDefaultAsync(u => u.UserId == UserId);

            if(user is not null) 
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("DisplayUsers", "Users");
        }
    }
}
