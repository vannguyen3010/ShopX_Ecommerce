namespace Shared.DTO.Cart
{
    public class AddToCartDto
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
