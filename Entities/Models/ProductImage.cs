namespace Entities.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageFileExtension { get; set; }
        public long ImageFileSizeInBytes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Product Product { get; set; }
        
    }
}
