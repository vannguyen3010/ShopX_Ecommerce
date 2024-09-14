namespace Shared.DTO.Cart
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public string ImageFilePath { get; set; }
    }
    public class CartDtos
    {
        public IEnumerable<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
