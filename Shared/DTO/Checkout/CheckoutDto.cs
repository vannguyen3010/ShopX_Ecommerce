using Shared.DTO.Address;
using Shared.DTO.Cart;
using Shared.DTO.Payment;

namespace Shared.DTO.Checkout
{
    public class CheckoutDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public AddressDto Address { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public PaymentMethodDto PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public bool OrderStatus { get; set; } // true = Paid, false = Unpaid
        //public class OrderDto
        //{
        //    public Guid OrderId { get; set; }
        //    public Guid UserId { get; set; }
        //    public string UserName { get; set; }
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; }
        //    public string Email { get; set; }
        //    public string PhoneNumber { get; set; }
        //    public string Note { get; set; }
        //    public AddressDto Address { get; set; }
        //    public IEnumerable<CartItemDto> CartItems { get; set; }
        //    public PaymentMethodDto PaymentMethod { get; set; }
        //    public decimal TotalAmount { get; set; }
        //    public bool OrderStatus { get; set; } // true = Paid, false = Unpaid
        //}
    }
}
