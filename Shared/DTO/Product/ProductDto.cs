using Shared.DTO.CateProduct;

namespace Shared.DTO.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CateProductDto ParentCategory { get; set; }
        public string ImageFilePath { get; set; }
        public bool IsHot { get; set; }
        public int Rating { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
    }
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageFilePath { get; set; }
    }
}
