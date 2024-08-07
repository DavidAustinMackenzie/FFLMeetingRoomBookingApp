﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FFLMeetingRoomBookingApp.Web.Data;
using FFLMeetingRoomBookingApp.Web.Models;
using Microsoft.Graph.Models;
using Azure.Identity;
using Microsoft.Graph;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.Options;

namespace FFLMeetingRoomBookingApp.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly FFLMeetingRoomBookingAppWebContext _context;

        public BookingsController(FFLMeetingRoomBookingAppWebContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var fFLMeetingRoomBookingAppWebContext = _context.Booking.Include(b => b.MeetingRoom).Include(b => b.User);
            return View(await fFLMeetingRoomBookingAppWebContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.MeetingRoom)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["MeetingRoomId"] = new SelectList(_context.MeetingRoom, "MeetingRoomId", "MeetingRoomName");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName");
            ViewData["BookedBy"] = new SelectList(_context.User, "FullName", "FullName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,MeetingRoomId,UserId,StartDate,EndDate,NumberOfPeople,Participants,CreatedAt,UpdatedAt,BookedBy")] Booking booking)
        {
            if (booking.StartDate == booking.EndDate)
            {
                ModelState.AddModelError("StartDate",
                                          "The start and end meeting can't be the same.");
            }

            if (booking.EndDate < booking.StartDate)
            {
                ModelState.AddModelError("EndDate",
                                          "The meeting finish time must be after that start time.");
            }


            if (_context.Booking.Any(b => b.MeetingRoomId == booking.MeetingRoomId 
                && b.StartDate == booking.StartDate))
            {
                ModelState.AddModelError("StartDate",
                                          "The meeting room is already booked for this time.");
            }

            if (ModelState.IsValid)
            {
                booking.CreatedAt = DateTime.Now;
                booking.UpdatedAt = DateTime.Now;
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingRoomId"] = new SelectList(_context.MeetingRoom, "MeetingRoomId", "MeetingRoomName", booking.MeetingRoomId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", booking.UserId);
            ViewData["BookedBy"] = new SelectList(_context.User, "FullName", "FullName", booking.BookedBy);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["MeetingRoomId"] = new SelectList(_context.MeetingRoom, "MeetingRoomId", "MeetingRoomName", booking.MeetingRoomId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", booking.UserId);
            ViewData["BookedBy"] = new SelectList(_context.User, "FullName", "FullName",booking.BookedBy);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,MeetingRoomId,UserId,StartDate,EndDate,NumberOfPeople,Participants,CreatedAt,UpdatedAt,BookedBy")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    booking.UpdatedAt = DateTime.Now;
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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
            ViewData["MeetingRoomId"] = new SelectList(_context.MeetingRoom, "MeetingRoomId", "MeetingRoomName", booking.MeetingRoomId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", booking.UserId);
            ViewData["BookedBy"] = new SelectList(_context.User, "FullName", "FullName", booking.BookedBy);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.MeetingRoom)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Bookings/SendInvite/5
        public async Task<IActionResult> SendInvite(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.MeetingRoom)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/SendInviteConfirmed
        public async Task<IActionResult> SendInviteConfirmed()
        {
            /*var scopes = new[] { "User.Read" };
            var clientId = "YOUR_CLIENT_ID";
            var tenantId = "common";

            // using Azure.Identity;
            var options = new DeviceCodeCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                ClientId = clientId,
                TenantId = tenantId,
                // Callback function that receives the user prompt
                // Prompt contains the generated device code that user must
                // enter during the auth process in the browser
                DeviceCodeCallback = (code, cancellation) =>
                {
                    Console.WriteLine(code.Message);
                    return Task.FromResult(0);
                },
            };

            var deviceCodeCredential = new DeviceCodeCredential(options);

            var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);

            var requestBody = new OnlineMeeting
            {
                StartDateTime = DateTimeOffset.Parse("2019-07-12T14:30:34.2444915-07:00"),
                EndDateTime = DateTimeOffset.Parse("2019-07-12T15:00:34.2464912-07:00"),
                Subject = "User Token Meeting",
            };

            // To initialize your graphClient, see https://learn.microsoft.com/en-us/graph/sdks/create-client?from=snippets&tabs=csharp
            var result = await graphClient.Me.OnlineMeetings.PostAsync(requestBody);*/

            return View();
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
    }
}
