using Entities.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

        public async Task<ApiResponse<OrderResponse>> GetListOrderByUserId(string userId, string keyword = null, int pageNumber = 1, int pageSize = 10)
        {
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(keyword))
            {
                queryParameters["keyword"] = keyword;
            }

            var query = QueryHelpers.AddQueryString($"/api/Order/GetListOrdersByUserId/{userId}", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderResponse>>();
                return result;
            }
            return null;
        }
    }
}
