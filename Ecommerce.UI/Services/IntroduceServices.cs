using Shared.DTO.Introduce;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class IntroduceServices
    {
        private readonly HttpClient _httpClient;

        public IntroduceServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<ApiResponse<IntroduceResponse>> GetListIntroduceAsync(int pageNumber = 1, int pageSize = 10, Guid? categoryId = null, string? keyword = null)
        {
            var query = $"/api/Introduce/GetListIntroduce?pageNumber={pageNumber}&pageSize={pageSize}";

            if (categoryId.HasValue)
            {
                query += $"&categoryId={categoryId.Value}";
            }

            if(!string.IsNullOrEmpty(keyword))
            {
                query += $"&keyword={keyword}";
            }

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IntroduceResponse>>();
                return result;
            }
            return null;
        }
        public async Task<ApiProductResponse<IntroduceDto, IEnumerable<IntroduceDto>>> GetIntroduceByIdAsync(Guid Id)
        {
            var response = await _httpClient.GetAsync($"api/Introduce/GetIntroduceById/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiProductResponse<IntroduceDto, IEnumerable<IntroduceDto>>>();
            }
            return null;
        }
        public async Task<IEnumerable<IntroduceDto>> GetListIntroduceIsHotAsync()
        {
            var response = await _httpClient.GetAsync("/api/Introduce/GetAllIntroduceIsHot");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<IntroduceDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<IntroduceDto>();
            }
            else
            {
                return Enumerable.Empty<IntroduceDto>();
            }
        }
    }
}
