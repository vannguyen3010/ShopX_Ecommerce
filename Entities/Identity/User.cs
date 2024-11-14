using Microsoft.AspNetCore.Identity;

namespace Entities.Identity
{
    public class User : IdentityUser
    {
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public string? VerificationCode { get; set; }
    }
}
