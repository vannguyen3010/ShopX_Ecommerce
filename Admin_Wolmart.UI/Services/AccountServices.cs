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
        private readonly CookieService _cookieService;
        private readonly NavigationManager _navigationManager;

        public AccountServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, CookieService cookieService, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _cookieService = cookieService;
            _navigationManager = navigationManager;
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

            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (result == null) return false;

            // Lưu token vào Cookie
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(5), // Thời gian hết hạn cookie
                Secure = _httpContextAccessor.HttpContext.Request.IsHttps, // Chỉ áp dụng Secure khi sử dụng HTTPS
                SameSite = SameSiteMode.Strict
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("accessToken", result.Token, options);

            // Lưu Refresh Token
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", result.RefreshTokens, options);
            return true;
        }


        private async Task<string> RefreshTokenIfExpired()
        {
            var accessToken = _cookieService.Get("AccessToken");
            var refreshToken = _cookieService.Get("RefreshToken");

            // Kiểm tra nếu Access Token đã hết hạn
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadJwtToken(accessToken);
            if (token.ValidTo < DateTime.UtcNow)
            {
                // Gửi yêu cầu refresh token
                var refreshRequest = new RefreshTokenDto { RefreshTokens = refreshToken };
                var response = await _httpClient.PostAsJsonAsync("api/Auth/RefreshToken", refreshRequest);

                if (response.IsSuccessStatusCode)
                {
                    var refreshResponse = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>();
                    if (refreshResponse != null)
                    {
                        // Cập nhật Access Token và Refresh Token
                        var options = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        };

                        _cookieService.Set("AccessToken", refreshResponse.Token, options);
                        _cookieService.Set("RefreshToken", refreshResponse.RefreshTokens, options);

                        return refreshResponse.Token;
                    }
                }
                else
                {
                    // Token hết hạn, chuyển hướng về trang đăng nhập
                    _navigationManager.NavigateTo("/login");
                }
            }

            return accessToken;
        }

        public async Task<HttpResponseMessage> CallApiWithTokenRefresh(HttpRequestMessage request)
        {
            var token = await RefreshTokenIfExpired(); // Refresh token nếu cần
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.SendAsync(request);
        }
    }
}