using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.Payment
{
    public class PaymentMethodDto
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Note { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
