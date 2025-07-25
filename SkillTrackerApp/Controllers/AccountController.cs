using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
