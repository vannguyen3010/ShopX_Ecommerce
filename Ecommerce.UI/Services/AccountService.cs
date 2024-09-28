using Shared.DTO.Response;
using Shared.DTO.User;
namespace Ecommerce.UI.Services
{
    public class AccountService
    {
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

        public async Task<AuthResponseDto> Login(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginUser", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                return result ?? new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "No data received" };
            }
            else
            {
                return new AuthResponseDto
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = $"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}"
                };
            }
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var forgotPasswordDto = new ForgotPasswordDto
            {
                Email = email,
                ClientURL = "string"
            };

            var response = await _httpClient.PostAsJsonAsync("api/Accounts/ForgotPassword", forgotPasswordDto);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/ResetPassword", request);
            return response.IsSuccessStatusCode;
        }
    }
}
