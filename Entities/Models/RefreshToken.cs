using Entities.Identity;

namespace Entities.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string? Token { get; set; }
        public string? RefreshTokens { get; set; }
        public DateTime Expiration { get; set; } // Thời gian hết hạn của refresh token. Sau thời gian này, refresh token không còn hợp lệ.
        public bool IsUsed { get; set; }  // Cờ để đánh dấu xem refresh token đã được sử dụng để cấp phát một token mới hay chưa. Nếu đã sử dụng, giá trị sẽ là true.
        public bool IsRevoked { get; set; }  // Cờ để đánh dấu xem refresh token đã bị thu hồi hay chưa. Nếu bị thu hồi, giá trị sẽ là true và token không còn hợp lệ.
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
