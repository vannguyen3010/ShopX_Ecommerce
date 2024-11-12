using Microsoft.AspNetCore.Http;

namespace Shared.DTO.SocialMediaInfo
{
    public class UpdateSocialMediaInfoDto
    {
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string TikTokLink { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IFormFile? File { get; set; }
    }
}
