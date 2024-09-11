namespace Shared.DTO.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
