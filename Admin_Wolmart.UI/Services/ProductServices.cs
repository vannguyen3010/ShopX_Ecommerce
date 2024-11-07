using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<ProductResponseDto>> GetAllProductsPagination(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetAllProductsPagination?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return data!;
            }

            return new ApiResponse<ProductResponseDto>();
        }

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllProductBestSeller(int bestSeller)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetAllBestSellingProducts?bestSeller={bestSeller}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
            }
            return null;
        }

        public async Task<ApiResponse<ProductResponseDto>> GetListProductAsync(int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            // Tạo URL gốc cho API
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };


            if (categoryId.HasValue)
            {
                queryParameters["categoryId"] = categoryId.Value.ToString();
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                queryParameters["keyword"] = keyword;
            }


            // Sử dụng QueryHelpers để tạo chuỗi truy vấn
            var query = QueryHelpers.AddQueryString("/api/Product/GetListProduct", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return result;
            }

            return null;
        }

        public async Task<bool> UpdateProductStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateCateIntroDtoStatus { Status = status };
            var query = $"/api/Product/UpdateProductStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Product/DeleteProductById/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
