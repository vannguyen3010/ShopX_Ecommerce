using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class CateProductServices
    {
        private readonly HttpClient _httpClient;

        public CateProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CateProductDto>> GetAllCategoryProductsHaveProduct()
        {
            var response = await _httpClient.GetAsync("/api/CateProduct/GetAllCategoryProductsHaveProduct");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<CateProductDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<CateProductDto>();
            }
            else
            {
                return Enumerable.Empty<CateProductDto>();
            }
        }

        public async Task<ApiResponse<CateProductResponseDto>> GetAllCategoryProductPaginationAsync(int pageNumber, int pageSize, string? keyword = null)
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

            var query = QueryHelpers.AddQueryString("/api/CateProduct/GetAllCategoryProductsPage", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CateProductResponseDto>>();
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateCategoryProductStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateCateIntroDtoStatus { Status = status };
            var query = $"/api/CateProduct/UpdateCategoryProductStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }
    }
}
