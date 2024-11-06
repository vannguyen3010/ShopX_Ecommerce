using Microsoft.AspNetCore.Components.Forms;
using Shared;
using Shared.DTO.Banner;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Introduce;
using Shared.DTO.Response;
using System.Net.Http.Headers;

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

        public async Task<ApiResponse<BannerDto>> GetBannerByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Banner/GetBannerById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var banner = await response.Content.ReadFromJsonAsync<ApiResponse<BannerDto>>();
                return banner;
            }
            return null;
        }

        public async Task<bool> UpdateBannerAsync(Guid id, BannerUpdateDto request, IBrowserFile? file, IBrowserFile? secondFile)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.Title), "Title");
            content.Add(new StringContent(request.Desc), "Desc");
            content.Add(new StringContent(request.Position.ToString()), "Position");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }
            if (secondFile != null)
            {
                var fileSecondContent = new StreamContent(secondFile.OpenReadStream(10485760)); // 10MB limit
                fileSecondContent.Headers.ContentType = new MediaTypeHeaderValue(secondFile.ContentType);
                content.Add(fileSecondContent, "SecondFile", secondFile.Name);
            }

            var response = await _httpClient.PutAsync($"/api/Banner/UpdateBanner/{id}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
