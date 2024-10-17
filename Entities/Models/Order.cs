using System.Text.Json.Serialization;
using AddressEntity = Entities.Models.Address.Address;


namespace Entities.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid ShippingCostId { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool OrderStatus { get; set; } 
        public string Note { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.Now;


        [JsonIgnore]
        public AddressEntity Address { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [JsonIgnore]
        public IEnumerable<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
