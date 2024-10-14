using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class BaseServices
    {
        private readonly HttpClient _httpClient;

        public BaseServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CateProductDto>> GetAllCategoryProducts()
        {
            var response = await _httpClient.GetAsync("/api/CateProduct/GetAllCategoryProducts");
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
        public async Task<IEnumerable<CategoryIntroduceDto>> GetAllCategoryIntroduces()
        {
            var response = await _httpClient.GetAsync("/api/CategoryIntroduce/GetAllCategory");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<CategoryIntroduceDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<CategoryIntroduceDto>();
            }
            else
            {
                return Enumerable.Empty<CategoryIntroduceDto>();
            }
        }

    }
}
