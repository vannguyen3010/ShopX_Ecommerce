using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Introduce;
using Shared.DTO.Response;
using System.Net.Http.Headers;

namespace Admin_Wolmart.UI.Services
{
    public class CateProductServices
    {
        private readonly HttpClient _httpClient;

        public CateProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CateProductDto>> GetAllCategoryProductsHaveProduct()
        {
            var response = await _httpClient.GetAsync("/api/CateProduct/GetAllCategoryProductsHaveProduct");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<CateProductDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<CateProductDto>();
            }
            else
            {
                return Enumerable.Empty<CateProductDto>();
            }
        }

        public async Task<ApiResponse<CateProductResponseDto>> GetAllCategoryProductPaginationAsync(int pageNumber, int pageSize, string? keyword = null)
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

            var query = QueryHelpers.AddQueryString("/api/CateProduct/GetAllCategoryProductsPage", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CateProductResponseDto>>();
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateCategoryProductStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateCateIntroDtoStatus { Status = status };
            var query = $"/api/CateProduct/UpdateCategoryProductStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/CateProduct/DeleteCategoryProduct/{id}");

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> UpdateCategoryIntroduceAsync(Guid id, UpdateCateProductDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/CateProduct/UpdateCategoryProductStatus/{id}", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResponse<CateProductDto>> GetCateProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/CateProduct/GetCategoryProductByCategoryId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<ApiResponse<CateProductDto>>();
                return category;
            }
            return null;
        }

        public async Task<bool> UpdateCateProductAsync(Guid id, UpdateCateProductDto request, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.Name ?? ""), "Name");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }

            var response = await _httpClient.PutAsync($"/api/CateProduct/UpdateCategoryProduct/{id}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
