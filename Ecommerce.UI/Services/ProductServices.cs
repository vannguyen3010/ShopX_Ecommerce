using Shared.DTO.Banner;
using Shared;
using Shared.DTO.Product;
using Shared.DTO.Response;
using Shared.DTO.BannerProduct;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.UI.Services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BannerProductDto>> GetAllBannerPositionProductPopup(BannerProductWithPopupPosition position)
        {
            var response = await _httpClient.GetAsync($"/api/BannerProduct/GetAllBannerPositionProductPopup?position={position}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<BannerProductDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data;
            }
            return new List<BannerProductDto>();
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
        public async Task<ApiResponse<ProductDto>> GetProductById(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ProductDto>>($"/api/Product/GetProductById/{id}");
            return response;
        }
    }
}
