using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Payment
{
    public class UpdatePaymentDto
    {
        public string PaymentType { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Note { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }

    public class UpdatePaymentStatusDto
    {
        public bool Status { get; set; }
    }
}
