using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Nhập địa chỉ Email!")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }

        //public string? ClientURI { get; set; }

    }
}
