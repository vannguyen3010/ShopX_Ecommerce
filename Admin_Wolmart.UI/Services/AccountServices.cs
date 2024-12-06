using Shared.DTO.Response;
using Shared.DTO.User;

namespace Admin_Wolmart.UI.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/api/Accounts/GetAllUsers");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>();
                return data!;
            }
            return null;
        }

        //public async Task<AuthResponseDto> LoginAsync(LoginDto request)
        //{
        //    var result = await _httpClient.PostAsJsonAsync($"/api/Accounts/LoginAdmin", request);
        //    if (!result.IsSuccessStatusCode)
        //    {
        //        return new AuthResponseDto
        //        {
        //            IsAuthSuccessful = false,
        //        };
        //    }
        //    var response = await result.Content.ReadFromJsonAsync<AuthResponseDto>();
        //    return response!;
        //}

        public async Task<AuthResponseDto> LoginAsync(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginAdmin", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                if (result.IsAuthSuccessful)
                {
                    var cookieOptions = new CookieOptions { HttpOnly = true, Secure = true };

                    _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", result.Token, cookieOptions);

                    return new AuthResponseDto
                    {
                        IsAuthSuccessful = true,
                    };
                }
            }

            return new AuthResponseDto
            {
                IsAuthSuccessful = false,
            };
        }

    }
}