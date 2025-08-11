using Microsoft.AspNetCore.Authorization;
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

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        // Update goal progress
        public async Task<IActionResult> UpdateGoalProgressAsync(int goalId, int progress)
        {
            var goal = await _context.LearningGoals.FindAsync(goalId);
            if (goal != null)
            {
                goal.ProgressPercentage = progress;
                goal.IsCompleted = (progress == 100);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Goals"); // Redirect to goals page
        }

        // Get average progress for a specific user
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
