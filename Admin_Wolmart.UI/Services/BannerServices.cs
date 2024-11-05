using Shared;
using Shared.DTO.Banner;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class BannerServices
    {
        private readonly HttpClient _httpClient;

        public BannerServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<BannerDto>>> GetAllBannerPositionAsync(BannerPosition? position = null)
        {
            string url = "/api/Banner/GetAllBannerPosition";

            if (position.HasValue)
            {
                url += $"?position={(int)position.Value}";
            }

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<BannerDto>>>(url);
            return response;
        }

        public async Task<bool> UpdateBannerStatusAsync(Guid id, bool status)
        {
            var updateStatus = new BannerUpdateStatusDto { Status = status };
            var query = $"/api/Banner/UpdateBannerStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBannerAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Banner/DeleteBanner/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
