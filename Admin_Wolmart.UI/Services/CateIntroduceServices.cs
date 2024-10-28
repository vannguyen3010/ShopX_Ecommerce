using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class CateIntroduceServices
    {
        private readonly HttpClient _httpClient;

        public CateIntroduceServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<IntroduceCategoryResponse>> GetAllCategoryPaginationAsync(int pageNumber, int pageSize, string? keyword = null)
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

            var query = QueryHelpers.AddQueryString("/api/CategoryIntroduce/GetAllCategoryPagination", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IntroduceCategoryResponse>>();
                return result;
            }
            return null;
        }
    }
}
