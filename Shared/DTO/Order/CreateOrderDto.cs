using Shared.DTO.Address;
using Shared.DTO.Cart;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Shared.DTO.Order
{
    public class CreateOrderDto
    {
        [Required]
        public string UserId { get; set; } // ID của người dùng
        [Required]
        public Guid AddressId { get; set; } // ID của địa chỉ giao hàng
        [Required]
        public Guid PaymentMethodId { get; set; } // ID của phương thức thanh toán
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Note { get; set; } // Ghi chú đơn hàng
    }
   
}
