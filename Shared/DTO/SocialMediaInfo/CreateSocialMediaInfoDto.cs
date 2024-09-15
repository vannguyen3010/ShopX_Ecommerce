using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.SocialMediaInfo
{
    public class CreateSocialMediaInfoDto
    {
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string TikTokLink { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile LogoUrl { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
