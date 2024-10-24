using Shared.DTO.Address;
using Shared.DTO.Order;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class OrderServices
    {
        private readonly HttpClient _httpClient;

        public OrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<OrderDto>>> GetListOrderTypeAsync(int type)
        {
            var response = await _httpClient.GetAsync($"/api/Order/GetListOrdersType?type={type}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<OrderDto>>>();
                return result;
            }
            return new ApiResponse<IEnumerable<OrderDto>>
            {
                Success = false,
                Message = "Failed to retrieve orders."
            };
        }
    }
}
