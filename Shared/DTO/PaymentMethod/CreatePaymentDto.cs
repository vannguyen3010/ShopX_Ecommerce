using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Payment
{
    public class CreatePaymentDto
    {
        public string PaymentType { get; set; }// Tên phương thức thanh toán (VD: COD, QR code)
        public string BankName { get; set; }// Tên ngân hàng
        public string AccountNumber { get; set; }
        public string Note { get; set; } // Số tài khoản ngân hàng
        public IFormFile File { get; set; }
    }
}
