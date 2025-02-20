using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using osprey_web_app.Areas.Identity.Data;
using osprey_web_app.Data;
using osprey_web_app.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace osprey_web_app.Controllers
{
    [Authorize] // 🔒 Restrict access to authenticated users
    public class WorkshopController : Controller
    {
        private readonly osprey_web_appContext _context;
        private readonly UserManager<osprey_web_appUser> _userManager;

        public WorkshopController(osprey_web_appContext context, UserManager<osprey_web_appUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var workshops = _context.Workshops
                .OrderBy(w => w.Date)
                .AsNoTracking()
                .ToList();

            var joinedWorkshops = _context.UserWorkshops
                .Where(uw => uw.UserId == user.Id)
                .Select(uw => uw.WorkshopId)
                .ToList();

            ViewBag.JoinedWorkshops = joinedWorkshops; // Send the list of joined workshops to the view

            return View("~/Views/Home/Workshops.cshtml", workshops);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Date,Description")] Workshop workshop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Workshops.Add(workshop);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Workshop created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = $"Error saving workshop: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RSVP(int workshopId, string whatsAppNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You must be logged in to RSVP.";
                return RedirectToAction(nameof(Index));
            }

            var workshop = await _context.Workshops.FindAsync(workshopId);
            if (workshop == null)
            {
                TempData["Error"] = "Workshop not found.";
                return RedirectToAction(nameof(Index));
            }

            // Check if the user is already registered
            bool isAlreadyRSVPd = _context.UserWorkshops
                .Any(uw => uw.UserId == user.Id && uw.WorkshopId == workshopId);

            if (isAlreadyRSVPd)
            {
                TempData["Error"] = "You have already RSVP'd for this workshop.";
                return RedirectToAction(nameof(Index));
            }

            // Save the RSVP
            var userWorkshop = new UserWorkshop
            {
                UserId = user.Id,
                WorkshopId = workshopId,
                WhatsAppNumber = whatsAppNumber
            };

            _context.UserWorkshops.Add(userWorkshop);
            await _context.SaveChangesAsync();

            TempData["Success"] = "You have successfully RSVP'd!";
            return RedirectToAction(nameof(Index));
        }
    }
}
