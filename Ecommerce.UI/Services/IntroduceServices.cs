using Shared.DTO.Introduce;
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
        
        public async Task<ApiResponse<IntroduceResponse>> GetListIntroduce(int pageNumber = 1, int pageSize = 10, Guid? categoryId = null, string? keyword = null)
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
    }
}
