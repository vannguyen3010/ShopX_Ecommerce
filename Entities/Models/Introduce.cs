using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Introduce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }

        [ForeignKey("CategoryIntroduce")]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileExtension { get; set; }
        public long? FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public bool IsHot { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public CategoryIntroduce CategoryIntroduce { get; set; }
    }
}
