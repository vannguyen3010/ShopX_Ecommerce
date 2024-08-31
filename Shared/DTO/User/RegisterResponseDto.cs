namespace Shared.DTO.User
{
    public class RegisterResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
