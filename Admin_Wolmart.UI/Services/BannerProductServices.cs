using Shared.DTO.Banner;
using Shared.DTO.Response;
using Shared;
using Shared.DTO.BannerProduct;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace Admin_Wolmart.UI.Services
{
    public class BannerProductServices
    {
        private readonly HttpClient _httpClient;

        public BannerProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<BannerProductDto>>> GetAllBannerPositionAsync(BannerProductWithPopupPosition? position = null)
        {
            string url = "/api/BannerProduct/GetAllBannerPositionProductPopup";

            if (position.HasValue)
            {
                url += $"?position={(int)position.Value}";
            }

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<BannerProductDto>>>(url);
            return response;
        }

        public async Task<bool> UpdateBannerProductStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateBannerProductStatusDto { Status = status };
            var query = $"/api/BannerProduct/UpdateBannerProductStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBannerProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/BannerProduct/DeleteBannerProduct/{id}");

            return response.IsSuccessStatusCode;

        }
        public async Task<ApiResponse<BannerProductDto>> GetBannerProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/BannerProduct/GetBannerProductById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var banner = await response.Content.ReadFromJsonAsync<ApiResponse<BannerProductDto>>();
                return banner;
            }
            return null;
        }

        public async Task<bool> UpdateBannerAsync(Guid id, UpdateBannerProductDto request, IBrowserFile? file)
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

            var response = await _httpClient.PutAsync($"/api/BannerProduct/UpdateBannerProduct/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateBannerProductAsync(CreateBannerProductDto request, IBrowserFile? file)
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

            var response = await _httpClient.PostAsync("/api/BannerProduct/CreateBannerProduct", content);
            return response.IsSuccessStatusCode;
        }
    }
}
