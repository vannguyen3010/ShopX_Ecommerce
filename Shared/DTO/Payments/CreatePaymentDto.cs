using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Payments
{
    public class CreatePaymentDto
    {
        [Required]
        public Guid OrderId { get; set; }
        //public string PaymentMethod { get; set; }
        //public decimal totalAmount { get; set; }
    }
}
