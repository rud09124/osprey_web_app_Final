using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using osprey_web_app.Data;
using osprey_web_app.Models;
using System.Linq;

namespace osprey_web_app.Controllers
{
    [Authorize] // 🔒 Restrict access to logged-in users only
    public class MentorController : Controller
    {
        private readonly osprey_web_appContext _context;

        public MentorController(osprey_web_appContext context)
        {
            _context = context;
        }

        // Display the list of Mentors
        public IActionResult Index()
        {
            var mentors = _context.Mentors.ToList();
            return View("~/Views/Home/Mentor.cshtml", mentors);
        }

        // Add Mentor (GET)
        public IActionResult AddMentor()
        {
            return View();
        }

        // Add Mentor (POST)
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ Prevents CSRF attacks
        public IActionResult AddMentor(Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                _context.Mentors.Add(mentor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mentor);
        }
    }
}
