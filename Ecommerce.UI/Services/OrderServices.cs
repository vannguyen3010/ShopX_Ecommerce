using Entities.Models.Address;
using Shared.DTO.Address;
using Shared.DTO.Order;
using Shared.DTO.Payment;
using Shared.DTO.Product;
using Shared.DTO.Response;
using System.Collections.Generic;

namespace Ecommerce.UI.Services
{
    public class OrderServices
    {
        private readonly HttpClient _httpClient;

        public OrderServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<OrderDto>> CreateOrderAsync(CreateOrderDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Order/CreateOrder", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
            }
            else
            {
                var errorResponse = new ApiResponse<OrderDto>
                {
                    Success = false,
                    Message = "Failed to create order. Please try again."
                };
                return errorResponse;
            }
        }

        public async Task<ApiResponse<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethodsAsync()
        {
            var response = await _httpClient.GetAsync("/api/PaymentMethod/GetAllPaymentMethod");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PaymentMethodDto>>>();
            }
            else
            {
                var errorResponse = new ApiResponse<IEnumerable<PaymentMethodDto>>
                {
                    Success = false,
                    Message = "Failed to retrieve payment methods."
                };
                return errorResponse;
            }
        }

        public async Task<ApiResponse<OrderDto>> GetOrderByIdAsync(Guid orderId)
        {
            var response = await _httpClient.GetAsync($"/api/Order/GetOrderById/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<OrderDto>>();
            }

            return null;
        }
    }
}
