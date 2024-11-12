using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Nhập Họ và tên.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "ConfirmPassword is required")]
        public string? ConfirmPassword { get; set; }
        //[Required]
        //public string? ClientURL { get; set; }
    }
}
