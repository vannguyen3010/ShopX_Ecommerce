using System.Text.Json.Serialization;
using AddressEntity = Entities.Models.Address.Address;


namespace Entities.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; } // ID của phương thức thanh toán
        public Guid ShippingCostId { get; set; } // ID của chi phí vận chuyển
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool OrderStatus { get; set; } // Trạng thái đơn hàng
        public string Note { get; set; }
        [JsonIgnore]
        public IEnumerable<CartItem> CartItems { get; set; }
        [JsonIgnore]
        public AddressEntity Address { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; } // Thông tin về chi phí vận chuyển
        public decimal TotalAmount { get; set; } // Tổng giá trị đơn hàng
        public DateTime OrderDate { get; set; } = DateTime.Now; // Ngày tạo đơn hàng
        public PaymentMethod PaymentMethod { get; set; }
    }
}
