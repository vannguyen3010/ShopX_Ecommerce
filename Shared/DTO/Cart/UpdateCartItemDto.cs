namespace Shared.DTO.Cart
{
    public class UpdateCartItemDto
    {
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
