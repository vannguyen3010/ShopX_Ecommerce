namespace Entities.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }  //(thời gian hết hạn của refresh token)
        public bool IsUsed { get; set; }    //(trạng thái cho biết token đã sử dụng chưa)
        public bool IsRevoked { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public string UserId { get; set; } // Khóa ngoại liên kết với người dùng
    }
}
