using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.SocialMediaInfo
{
    public class SocialMediaInfoDto
    {
        public Guid Id { get; set; }
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string TikTokLink { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
