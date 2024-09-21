using Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.User;

namespace Ecommerce.UI.Services
{
    //đây là lớp repo
    public class AccountService(HttpClient httpClient)
    {
        public async Task<RegisterResponseDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Accounts/Register", registerDto);

            if(response.IsSuccessStatusCode)
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

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Accounts/Login", loginDto);
            return response.IsSuccessStatusCode;
        }
    }
}
