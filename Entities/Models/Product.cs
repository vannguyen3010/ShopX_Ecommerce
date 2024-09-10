using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public CateProduct Category { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public List<IFormFile> ImageObjectList { get; set; } = new List<IFormFile>();
        public string ImageFileName { get; set; }
        public string? ImageDescription { get; set; }
        public string ImageFileExtension { get; set; }
        public long ImageFileSizeInBytes { get; set; }
        public string ImageFilePath { get; set; }
        public bool IsHot { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        // Thêm RowVersion cho đồng thời
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
