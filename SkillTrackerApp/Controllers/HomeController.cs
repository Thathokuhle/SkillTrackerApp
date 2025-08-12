using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;  // You injected UserManager but you don't have it here
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillTrackerApp.Database;
using SkillTrackerApp.Models;
using System.Diagnostics;

namespace SkillTrackerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;  // MISSING

        // Fix: Inject UserManager<Users> via constructor
        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<Users> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;  // Assign it here
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);  // Use _userManager, was missing in your class

            if (user == null)  // Defensive check
            {
                return Challenge();  // Force login if not authenticated properly
            }

            var goals = await _context.LearningGoals
                .Where(g => g.UserId == user.Id)
                .ToListAsync();

            var model = new DashboardViewModel
            {
                ActiveGoals = goals.Where(g => !g.IsCompleted).ToList(),
                CompletedCount = goals.Count(g => g.IsCompleted),
                InProgressCount = goals.Count(g => !g.IsCompleted)
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        // Update goal progress
        [HttpPost]  // Mark as POST since it modifies data
        [ValidateAntiForgeryToken]  // For security
        public async Task<IActionResult> UpdateGoalProgressAsync(int goalId, int progress)
        {
            var goal = await _context.LearningGoals.FindAsync(goalId);
            if (goal != null)
            {
                goal.ProgressPercentage = progress;
                goal.IsCompleted = (progress == 100);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index"); // Redirect to Index (Dashboard)
        }

        // You probably don’t need this as an action since it returns a primitive,
        // but if you want it as an API endpoint, mark with [HttpGet] and return Json
        public async Task<double> GetAverageProgressForUserAsync(string userId)
        {
            return await _context.LearningGoals
                .Where(g => g.UserId == userId)
                .AverageAsync(g => g.ProgressPercentage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
