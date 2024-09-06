using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public CateProduct Category { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageFileName { get; set; }
        public string? ImageDescription { get; set; }
        public string ImageFileExtension { get; set; }
        public long ImageFileSizeInBytes { get; set; }
        public string ImageFilePath { get; set; }
        public bool IsHot { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
