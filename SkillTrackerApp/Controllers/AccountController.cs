using Microsoft.AspNetCore.Mvc;
using SkillTrackerApp.ViewModels;

namespace SkillTrackerApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(); 
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }
        
    }
}
