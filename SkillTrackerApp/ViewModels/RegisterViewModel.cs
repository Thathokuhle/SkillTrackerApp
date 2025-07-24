using System.ComponentModel.DataAnnotations;

namespace SkillTrackerApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is requiered.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is requiered.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requiered.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} charector")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is requiered.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
