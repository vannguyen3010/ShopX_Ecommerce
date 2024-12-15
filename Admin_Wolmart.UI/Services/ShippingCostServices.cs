using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.Response;

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
    }
}
