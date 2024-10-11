using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class CateProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }

        [ForeignKey("ParentCategory")]
        public Guid? ParentCategoryId { get; set; }

        [JsonIgnore]
        public CateProduct ParentCategory { get; set; }

        public ICollection<CateProduct> CategoriesObjs { get; set; }
        public ICollection<Product> Products { get; set; }

        [NotMapped] // Sẽ ko lưu ảnh vào Db
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileExtension { get; set; }
        public long? FileSizeInBytes { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DateTime { get; set; }

    }
}
