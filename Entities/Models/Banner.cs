using Microsoft.AspNetCore.Http;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Banner
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileExtension { get; set; }
        public long? FileSizeInBytes { get; set; }
        public string? FilePath { get; set; }

        [NotMapped] // Sẽ không lưu ảnh vào Db
        public IFormFile? SecondFile { get; set; } // Trường ảnh thứ hai
        public string? SecondFileName { get; set; }
        public string? SecondFileExtension { get; set; }
        public long? SecondFileSizeInBytes { get; set; }
        public string? SecondFilePath { get; set; }

        [Required]
        public BannerPosition Position { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
   
}
