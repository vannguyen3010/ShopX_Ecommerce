using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
