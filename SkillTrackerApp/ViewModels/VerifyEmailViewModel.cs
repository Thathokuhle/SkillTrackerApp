using System.ComponentModel.DataAnnotations;

namespace SkillTrackerApp.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is requiered.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
