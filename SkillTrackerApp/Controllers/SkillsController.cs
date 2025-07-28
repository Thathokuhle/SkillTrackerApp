using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillTrackerApp.Database;
using SkillTrackerApp.Models;

namespace SkillTrackerApp.Controllers
{
    [Authorize]
    public class SkillsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public SkillsController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                
                skill.UserId = user.Id;

                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Skills");
            }
            return View(skill);
        }
     }
}
