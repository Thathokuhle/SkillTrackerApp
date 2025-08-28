using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerApp.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
