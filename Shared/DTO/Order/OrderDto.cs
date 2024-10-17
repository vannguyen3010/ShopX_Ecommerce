using Shared.DTO.Address;
using Shared.DTO.Cart;

namespace Shared.DTO.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid ShippingCostId { get; set; }
        public string OrderCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool OrderStatus { get; set; }
        public AddressDto Address { get; set; }
        //public IEnumerable<CartItemDto> CartItems { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
