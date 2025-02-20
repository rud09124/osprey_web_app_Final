using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using osprey_web_app.Data;
using osprey_web_app.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace osprey_web_app.Controllers
{
    [Authorize] // Ensure only logged-in users can access this controller
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly osprey_web_appContext _context;

        public HomeController(ILogger<HomeController> logger, osprey_web_appContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Fetch two upcoming workshops that are closest to today's date
            var workshops = _context.Workshops
                .Where(w => w.Date >= today)
                .OrderBy(w => w.Date)
                .Take(2)
                .ToList();

            // Get the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch the user's first name from the database
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            string firstName = user?.FirstName ?? "Guest"; // Default to "Guest" if not found

            // Pass data to the view using a ViewModel or ViewBag
            ViewBag.FirstName = firstName;
            ViewBag.Workshops = workshops;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Workshops()
        {
            var workshops = _context.Workshops.OrderBy(w => w.Date).ToList();
            return View(workshops);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
