using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillTrackerApp.Database;
using SkillTrackerApp.Models;

[Authorize]
public class GoalsController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<Users> _userManager;

    public GoalsController(AppDbContext context, UserManager<Users> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // READ ALL
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var goals = await _context.LearningGoals
            .Include(g => g.Skill)
            .Where(g => g.UserId == user.Id)
            .ToListAsync();
        return View(goals);
    }

    // CREATE GET
    public IActionResult Create()
    {
        ViewBag.Skills = _context.Skills.ToList();
        return View();
    }

    // CREATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("GoalTitle,TargetDate,IsCompleted,SkillId")] LearningGoal goal)
    {
        var user = await _userManager.GetUserAsync(User);
        goal.UserId = user.Id;

        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC sp_InsertGoal @GoalTitle={goal.GoalTitle}, @TargetDate={goal.TargetDate}, @IsCompleted={goal.IsCompleted}, @SkillId={goal.SkillId}, @UserId={goal.UserId}"
        );

        return RedirectToAction(nameof(Index));
    }

    // UPDATE GET
    public async Task<IActionResult> Update(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var goal = await _context.LearningGoals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);

        if (goal == null) return NotFound();

        ViewBag.Skills = _context.Skills.ToList();
        return View(goal);
    }

    // UPDATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [Bind("Id,GoalTitle,TargetDate,IsCompleted,SkillId")] LearningGoal goal)
    {
        var user = await _userManager.GetUserAsync(User);

        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC sp_UpdateGoal @Id={id}, @GoalTitle={goal.GoalTitle}, @TargetDate={goal.TargetDate}, @IsCompleted={goal.IsCompleted}, @SkillId={goal.SkillId}, @UserId={user.Id}"
        );

        return RedirectToAction(nameof(Index));
    }

    // DELETE GET
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var goal = await _context.LearningGoals.Include(g => g.Skill).FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);

        if (goal == null) return NotFound();

        return View(goal);
    }

    // DELETE POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _userManager.GetUserAsync(User);

        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"EXEC sp_DeleteGoal @Id={id}, @UserId={user.Id}"
        );

        return RedirectToAction(nameof(Index));
    }
}
