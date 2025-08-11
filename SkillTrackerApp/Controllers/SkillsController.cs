using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // READ ALL
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var skills = await _context.Skills
                .Where(s => s.UserId == user.Id)
                .ToListAsync();
            return View(skills);
        }

        // READ ONE
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var skill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")] Skill skill)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                skill.UserId = user.Id;

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC sp_InsertSkill @Name={skill.Name}, @Description={skill.Description}, @UserId={skill.UserId}"
                );

                return RedirectToAction("Index", "Skills");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to save skill.");
                return View(skill);
            }
        }

        // GET: Skills/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var skill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Description")] Skill skill)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC sp_UpdateSkill @Id={id}, @Name={skill.Name}, @Description={skill.Description}, @UserId={user.Id}"
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to update skill.");
                return View(skill);
            }
        }


        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var skill = await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == user.Id);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC sp_DeleteSkill @Id={id}, @UserId={user.Id}"
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to delete skill.");
                return View();
            }
        }


    }
}
