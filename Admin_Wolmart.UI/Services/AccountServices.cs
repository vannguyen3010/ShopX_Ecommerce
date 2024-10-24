using Shared.DTO.Response;
using Shared.DTO.User;

namespace Admin_Wolmart.UI.Services
{
    public class AccountServices
    {
        private readonly HttpClient _httpClient;

        public AccountServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
    }
}
