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

        public async Task<ApiResponse<IntroduceResponse>> GetListIntroduceTypeAsync(int pageNumber = 1, int pageSize = 10, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (categoryId.HasValue)
            {
                queryParameters["categoryId"] = categoryId.Value.ToString();
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                queryParameters["keyword"] = keyword;
            }

            if (type.HasValue)
            {
                queryParameters["type"] = type.Value.ToString();  // Thêm tham số type vào chuỗi truy vấn
            }

            var query = QueryHelpers.AddQueryString("/api/Introduce/GetListIntroduce", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IntroduceResponse>>();
                return result;
            }
            return null;
        }

        public async Task<ApiResponse<CategoryIntroduceDto>> GetCateIntroduceByCategoryIdAsync(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/CategoryIntroduce/GetCateIntroduceByCategoryId/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<CategoryIntroduceDto>>();
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

        public async Task<bool> UpdateIntroduceAsync(Guid id, UpdateIntroduceDto request, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.Name ?? ""), "Name");
            content.Add(new StringContent(request.Description ?? ""), "Description");
            content.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(request.IsHot.ToString()), "IsHot");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }

            var response = await _httpClient.PutAsync($"/api/Introduce/UpdateIntroduce/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateIntroduceAsync(CreateIntroduceDto introduce, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(introduce.Name ?? ""), "Name");
            content.Add(new StringContent(introduce.Description ?? ""), "Description");
            content.Add(new StringContent(introduce.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(introduce.IsHot.ToString()), "IsHot");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }

            // Gửi request
            var response = await _httpClient.PostAsync("/api/Introduce/CreateIntroduce", content);
            return response.IsSuccessStatusCode;
        }
    }
}

