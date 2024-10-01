using Shared.DTO.Banner;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class BannerServices
    {
        private readonly HttpClient _httpClient;

        public BannerServices(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BannerDto>> GetAllBannersAsync(int position = 0)
        {
            var response = await _httpClient.GetAsync($"/api/Banner/GetAllBannerPosition?position={position}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<BannerDto>>>();
            if(apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data;
            }
            return new List<BannerDto>();
        }
    }
}
