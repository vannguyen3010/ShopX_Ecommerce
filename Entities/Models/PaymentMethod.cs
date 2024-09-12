using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }// Tên phương thức thanh toán (VD: COD, QR code)
        public string BankName { get; set; }// Tên ngân hàng
        public string AccountNumber { get; set; } // Số tài khoản ngân hàng
        public string Note { get; set; }

        [NotMapped] // Sẽ ko lưu ảnh vào Db
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }// Đường dẫn tới ảnh QR code 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
