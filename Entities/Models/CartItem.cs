namespace Entities.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ImageFilePath { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Điều hướng thuộc tính
        public Product Product { get; set; } // Liên kết đến Product
    }
}
