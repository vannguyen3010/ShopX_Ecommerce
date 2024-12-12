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

        public async Task<bool> LoginAsync(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginAdmin", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

    }
}