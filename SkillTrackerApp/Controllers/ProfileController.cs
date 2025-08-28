using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
