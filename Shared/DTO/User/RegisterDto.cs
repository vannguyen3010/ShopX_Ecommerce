using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Họ là bắt buộc.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Tên lót là bắt buộc.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string? ConfirmPassword { get; set; }
        //[Required]
        //public string? ClientURL { get; set; }
    }
}
