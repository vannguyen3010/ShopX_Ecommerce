using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Contact
{
    public class CreateContactDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
