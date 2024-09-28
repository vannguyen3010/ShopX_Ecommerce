using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        [Required(ErrorMessage = "ConfirmPassword is required.")]
        public string? ConfirmPassword { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        //public string? Token { get; set; }
        [Required(ErrorMessage = "VerificationCode is required.")]
        public string? VerificationCode { get; set; }
    }
}
