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
    }
}
