using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class RegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "User is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string? ConfirmPassword { get; set; }
        //[Required]
        //public string? ClientURL { get; set; }
    }
}
