using Microsoft.AspNetCore.Identity;

namespace SkillTrackerApp.Models
{
    public class Users : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
