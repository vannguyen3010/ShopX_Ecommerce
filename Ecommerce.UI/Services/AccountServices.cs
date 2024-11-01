using Blazored.LocalStorage;
using Entities.Models;
using Shared.DTO.Response;
using Shared.DTO.User;
using System.Net.Http.Headers;
namespace Ecommerce.UI.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AccountServices(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/Register", registerDto);
            if (response.IsSuccessStatusCode)
            {
                return new RegisterResponseDto { IsSuccessfulRegistration = true };
            }

            // Nếu không thành công, đọc lỗi từ response
            var errorResponse = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();
            return new RegisterResponseDto
            {
                IsSuccessfulRegistration = false,
                Errors = errorResponse?.Errors,
                Message = errorResponse?.Message
            };

        }

        public async Task<AuthResponseDto> Login(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginUser", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                // Lưu UserId vào LocalStorage
                if (result != null && result.IsAuthSuccessful)
                {
                    await _localStorage.SetItemAsync("userId", result.UserId);
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    await _localStorage.SetItemAsync("refreshToken", result.RefreshTokens);

                }
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

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            if(!string.IsNullOrEmpty(refreshToken))
            {
                var refreshTokenDto = new { RefreshTokens = refreshToken };
                var response = await _httpClient.PostAsJsonAsync("api/Accounts/RefreshToken", refreshTokenDto);

                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                    // Cập nhật token mới vào local storage
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    await _localStorage.SetItemAsync("refreshToken", result.RefreshTokens);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                    return new AuthResponseDto() { IsAuthSuccessful = false };
                }
            }

            return new AuthResponseDto { IsAuthSuccessful = false };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<T> GetFromApi<T>(string uri)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không hợp lệ hoặc đã hết hạn.");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(uri);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Nếu nhận được mã trạng thái 401, hãy cố gắng làm mới token
                var refreshResult = await RefreshToken();

                if (refreshResult.IsAuthSuccessful)
                {
                    // Thực hiện lại yêu cầu sau khi làm mới token
                    return await GetFromApi<T>(uri);
                }
                else
                {
                    throw new UnauthorizedAccessException("Không thể làm mới token.");
                }
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var forgotPasswordDto = new ForgotPasswordDto
            {
                Email = email,
                ClientURL = "string"
            };

            var response = await _httpClient.PostAsJsonAsync("api/Accounts/ForgotPassword", forgotPasswordDto);
            if (response.IsSuccessStatusCode)
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

        public async Task<ApiResponse<object>> EmailConfirmationAsync(string email, string verificationCode)
        {
            var response = await _httpClient.GetAsync($"api/Accounts/EmailConfirmation?email={email}&verificationCode={verificationCode}");
            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<object> { Success = true, Message = "Email confirmed successfully!" };
            }
            //return new ApiResponse<object> { Success = false, Message = "Email confirmation failed!" };
            // Lấy thông tin lỗi từ API
            var errorMessage = await response.Content.ReadAsStringAsync();
            return new ApiResponse<object> { Success = false, Message = errorMessage };
        }

        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/Accounts/GetUserById/{userId}");

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
            }

            return null;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Accounts/ChangePassWord", request);

            return response.IsSuccessStatusCode;
        }
    }
}
