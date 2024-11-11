using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.Address;
using Shared.DTO.CategoryIntroduce;
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

        public async Task<ApiResponse<IEnumerable<OrderDto>>> GetListOrdersNewAsync(int days)
        {
            var response = await _httpClient.GetAsync($"/api/Order/GetListOrdersNew?days={days}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<OrderDto>>>();
            }
            return null;
        }

        public async Task<ApiResponse<OrderResponse>> GetListOrdersAsync(int pageNumber = 1, int pageSize = 10, int? type = null, string? orderCode = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(orderCode))
            {
                queryParameters["orderCode"] = orderCode;
            }

            if (type.HasValue)
            {
                queryParameters["type"] = type.Value.ToString();
            }

            var query = QueryHelpers.AddQueryString("/api/Order/GetOrdersList", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderResponse>>();
                return result;
            }
            return null;
        }

        public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid orderId)
        {
            var response = await _httpClient.GetAsync($"/api/Order/GetOrderById/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
                return res;
            }
            return null;
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid id, bool OrderStatus)
        {
            var updateStatus = new UpdateOrderDto { OrderStatus = OrderStatus };
            var query = $"/api/Order/UpdateOrderStatus/{id}?OrderStatus={OrderStatus}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Order/DeleteOrder/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
