using Admin_Wolmart.UI.Helpers;
using Microsoft.AspNetCore.Components;
using Shared.DTO.Response;
using Shared.DTO.User;
using System.IdentityModel.Tokens.Jwt;

namespace Admin_Wolmart.UI.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

        public AccountServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
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

        public async Task<bool> LoginAsync(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginAdmin", request);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    _customAuthenticationStateProvider.MarkUserAsAuthenticated(token);
                    // Lưu token vào Cookie
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("accesstoken_admin", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> LogoutAdminAsync()
        {
            var response = await _httpClient.PostAsync("/api/Accounts/LogoutAdmin", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}