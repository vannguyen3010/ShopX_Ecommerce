using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductIsHotAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/GetAllProductIsHot");
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<ProductDto>();
            }
            else
            {
                return Enumerable.Empty<ProductDto>();
            }
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductDiscountAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/GetAllProductDiscount");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<ProductDto>();
            }
            else
            {
                return Enumerable.Empty<ProductDto>();
            }
        }
    }
}
