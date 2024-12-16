using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Response;
using Shared.DTO.ShippingCost;

namespace Admin_Wolmart.UI.Services
{
    public class ShippingCostServices
    {
        private readonly HttpClient _httpClient;

        public ShippingCostServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<ShippingCostResponse>> GetListShippingCostAsync(int pageNumber, int pageSize, string? keyword = null)
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

            var query = QueryHelpers.AddQueryString("/api/ShippingCost/GetAllShippingCostPagination", queryParameters);

            var response = await _httpClient.GetAsync(query);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ShippingCostResponse>>();
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateShippingCostStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateStatusCostDto { Status = status };
            var query = $"/api/ShippingCost/UpdateStatusShippingCost/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }
        public async Task<ApiResponse<ShippingCostDto>> GetCategoryIntroduceByIdAsync(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/ShippingCost/GetShippingCostById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<ApiResponse<ShippingCostDto>>();
                return category;
            }
            return null;
        }

        public async Task<bool> UpdateShippingCostAsync(Guid id, UpdateCostDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/ShippingCost/UpdateShippingCost/{id}", request);
            return response.IsSuccessStatusCode;
        }

    }
}
