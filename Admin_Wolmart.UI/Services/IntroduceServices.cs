using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Introduce;
using Shared.DTO.Response;
using System.Net.Http.Headers;

namespace Admin_Wolmart.UI.Services
{
    public class IntroduceServices
    {
        private readonly HttpClient _httpClient;

        public IntroduceServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IntroduceResponse>> GetListIntroduceAsync(int pageNumber, int pageSize, string? keyword = null)
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

            var query = QueryHelpers.AddQueryString("/api/Introduce/GetAllIntroducesPagination", queryParameters);
            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IntroduceResponse>>();
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateIntroduceStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateCateIntroDtoStatus { Status = status };
            var query = $"/api/Introduce/UpdateIntroduceStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteIntroduceAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Introduce/DeleteIntroduce/{id}");

            return response.IsSuccessStatusCode;

        }

        public async Task<ApiProductResponse<IntroduceDto, object>> GetIntroduceByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Introduce/GetIntroduceById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<ApiProductResponse<IntroduceDto, object>>();
                return category;
            }
            return null;
        }

        public async Task<bool> UpdateIntroduceAsync(Guid id, UpdateIntroduceDto request)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(request.Name), "Name" },
                { new StringContent(request.CategoryId.ToString()), "CategoryId" },
                { new StringContent(request.Description), "Description" },
                { new StringContent(request.IsHot.ToString()), "IsHot" }
            };

            if (request.File != null)
            {
                var fileContent = new StreamContent(request.File.OpenReadStream());
                content.Add(fileContent, "File", request.File.Name);
            }

            var response = await _httpClient.PutAsync($"/api/Introduce/UpdateIntroduce/{id}", content);
            return response.IsSuccessStatusCode;
        }


    }
}
