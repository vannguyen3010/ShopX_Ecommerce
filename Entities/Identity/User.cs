using Microsoft.AspNetCore.Identity;

namespace Entities.Identity
{
    public class User : IdentityUser
    {
        public string? VerificationCode { get; set; }
    }
}
