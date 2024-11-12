using Microsoft.AspNetCore.Components.Forms;
using Shared.DTO.Banner;
using Shared.DTO.BannerProduct;
using Shared.DTO.Response;
using Shared.DTO.SocialMediaInfo;
using System.Net.Http.Headers;

namespace Admin_Wolmart.UI.Services
{
    public class ProfileServices
    {
        private readonly HttpClient _httpClient;

        public ProfileServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<SocialMediaInfoDto>> GetProfileAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/SocialMediaInfo/GetSocialMediaInfo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var social = await response.Content.ReadFromJsonAsync<ApiResponse<SocialMediaInfoDto>>();
                return social;
            }
            return null;
        }

        public async Task<bool> UpdateProfileAsync(Guid id, UpdateSocialMediaInfoDto request, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.FacebookLink), "FacebookLink");
            content.Add(new StringContent(request.ZaloLink), "ZaloLink");
            content.Add(new StringContent(request.TikTokLink), "TikTokLink");
            content.Add(new StringContent(request.Description), "Description");
            content.Add(new StringContent(request.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(request.Email), "Email");
            content.Add(new StringContent(request.Address), "Address");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }

            var response = await _httpClient.PutAsync($"/api/SocialMediaInfo/UpdateSocialMediaInfo/{id}", content);
            return response.IsSuccessStatusCode;
        }

    }
}
