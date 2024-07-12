using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFLMeetingRoomBookingApp.Web.Data;
using FFLMeetingRoomBookingApp.Web.Models;

namespace FFLMeetingRoomBookingApp.Web.Controllers
{
    public class MeetingRoomsController : Controller
    {
        private readonly FFLMeetingRoomBookingAppWebContext _context;

        public MeetingRoomsController(FFLMeetingRoomBookingAppWebContext context)
        {
            _context = context;
        }

        // GET: MeetingRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeetingRoom.ToListAsync());
        }

        // GET: MeetingRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRoom
                .FirstOrDefaultAsync(m => m.MeetingRoomId == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            return View(meetingRoom);
        }

        // GET: MeetingRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingRoomId,MeetingRoomName")] MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRoom.FindAsync(id);
            if (meetingRoom == null)
            {
                return NotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingRoomId,MeetingRoomName")] MeetingRoom meetingRoom)
        {
            if (id != meetingRoom.MeetingRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingRoomExists(meetingRoom.MeetingRoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRoom
                .FirstOrDefaultAsync(m => m.MeetingRoomId == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            return View(meetingRoom);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetingRoom = await _context.MeetingRoom.FindAsync(id);
            if (meetingRoom != null)
            {
                _context.MeetingRoom.Remove(meetingRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingRoomExists(int id)
        {
            return _context.MeetingRoom.Any(e => e.MeetingRoomId == id);
        }
    }
}
