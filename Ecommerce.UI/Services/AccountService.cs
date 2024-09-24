using Shared.DTO.User;
namespace Ecommerce.UI.Services
{
    public class AccountService
    {
        private const string BaseUrl = "api/Accounts";
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/Register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RegisterResponseDto>();
            }
            else
            {
                var errorResponse = new RegisterResponseDto
                {
                    Errors = new List<string> { "Đăng ký thất bại. Vui lòng kiểm tra lại thông tin." }
                };
                return errorResponse;
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/Login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                return authResponse;
            }
            // You can add detailed error handling here
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {errorContent}");
        }
    }
}
