using Shared.DTO.Address;
using Shared.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // ID của người dùng
        public Guid AddressId { get; set; } // ID của địa chỉ
        public Guid PaymentMethodId { get; set; } // ID của phương thức thanh toán
        public Guid ShippingCostId { get; set; } // ID của chi phí vận chuyển
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool OrderStatus { get; set; } // Trạng thái đơn hàng
        public AddressDto Address { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; } // Danh sách sản phẩm trong giỏ hàng
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; } // Thông tin về chi phí vận chuyển
        public decimal TotalAmount { get; set; } // Tổng giá trị đơn hàng
        public string Note { get; set; } // Ghi chú cho đơn hàng
        public DateTime OrderDate { get; set; } = DateTime.Now; // Ngày tạo đơn hàng
    }
}
