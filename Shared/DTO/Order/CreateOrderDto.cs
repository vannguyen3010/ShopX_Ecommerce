using System.ComponentModel.DataAnnotations;


namespace Shared.DTO.Order
{
    public class CreateOrderDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public Guid PaymentMethodId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Note { get; set; }
    }

}
