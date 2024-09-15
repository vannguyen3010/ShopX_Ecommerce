using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Payments
{
    public class PaymentProcessDto
    {
        [Required]
        public Guid OrderId { get; set; }
    }
}
