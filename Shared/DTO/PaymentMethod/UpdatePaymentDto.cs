using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Payment
{
    public class UpdatePaymentDto
    {
        public string PaymentType { get; set; }// Tên phương thức thanh toán (VD: COD, QR code)
        public string BankName { get; set; }// Tên ngân hàng
        public string AccountNumber { get; set; } // Số tài khoản ngân hàng
        public string Note { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
