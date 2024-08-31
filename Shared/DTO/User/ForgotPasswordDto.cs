using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.User
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? ClientURL { get; set; }
    }
}
