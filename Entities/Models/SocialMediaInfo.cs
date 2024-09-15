using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class SocialMediaInfo
    {
        public Guid Id { get; set; }
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string TikTokLink { get; set; }
        public string Description { get; set; }
        [NotMapped] // Sẽ ko lưu ảnh vào Db
        public IFormFile LogoUrl { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
