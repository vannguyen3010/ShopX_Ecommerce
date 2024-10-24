using System.Text.Json.Serialization;
using AddressEntity = Entities.Models.Address.Address;


namespace Entities.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid AddressId { get; set; }
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

        // Thông tin chi tiết của địa chỉ
        public string AddressLine { get; set; }
        public int AddressType { get; set; }

        // Thông tin phí ship
        public Guid PaymentMethodId { get; set; }
        public string PaymentType { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string NotePayment { get; set; }
        public string FilePath { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;


        [JsonIgnore]
        public IEnumerable<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
