using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Bạn cần phải nhập địa chỉ Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? ClientURL { get; set; }
    }
}
