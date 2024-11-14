namespace Shared.DTO.User
{
    //public class AuthResponseDto
    //{
    //    public bool IsAuthSuccessful { get; set; }
    //    public string? ErrorMessage { get; set; }
    //    public string? Token { get; set; }
    //}
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RefreshTokens { get; set; }
        public string? Token { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
    public class RefreshTokenResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RefreshTokens { get; set; }
        public string? Token { get; set; }
        public string UserId { get; set; }
    }
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
