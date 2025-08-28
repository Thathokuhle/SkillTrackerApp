using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillTrackerApp.Database;
using SkillTrackerApp.Models;
using SkillTrackerApp.Models.DTOs;
using System.Diagnostics;

namespace SkillTrackerApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<Users> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge(); // User not logged in

            // Fetch user's goals
            var goals = await _context.LearningGoals
                .Where(g => g.UserId == user.Id)
                .Select(g => new LearningGoal
                {
                    Id = g.Id,
                    GoalTitle = g.GoalTitle,
                    ProgressPercentage = g.ProgressPercentage ?? 0,
                    IsCompleted = g.IsCompleted,
                    TargetDate = g.TargetDate
                })
                .ToListAsync();

            // Fetch total skills
            var totalSkills = await _context.Skills.CountAsync(s => s.UserId == user.Id);

            // Recent activity (last 5 goals by target date descending)
            var recentActivities = goals
                .OrderByDescending(g => g.TargetDate)
                .Take(5)
                .Select(g => new RecentActivityDto
                {
                    Title = g.GoalTitle,
                    Date = g.TargetDate
                }).ToList();

            // Build DashboardViewModel
            var model = new DashboardViewModel
            {
                TotalSkills = totalSkills,
                TotalGoals = goals.Count,
                CompletedGoals = goals.Count(g => g.IsCompleted),
                CompletedCount = goals.Count(g => g.IsCompleted),
                InProgressCount = goals.Count(g => !g.IsCompleted),
                ActiveGoals = goals.Where(g => !g.IsCompleted).ToList(),
                RecentActivities = recentActivities
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGoalProgress(int goalId, int progress)
        {
            var goal = await _context.LearningGoals.FindAsync(goalId);
            if (goal != null)
            {
                goal.ProgressPercentage = progress;
                goal.IsCompleted = (progress >= 100);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<double> GetAverageProgressForUserAsync(string userId)
        {
            return await _context.LearningGoals
                .Where(g => g.UserId == userId)
                .AverageAsync(g => (double?)g.ProgressPercentage) ?? 0;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
