using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Product;
using Shared.DTO.Response;
using System.Net.Http.Headers;

namespace Admin_Wolmart.UI.Services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<ProductResponseDto>> GetAllProductsPagination(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetAllProductsPagination?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return data!;
            }

            return new ApiResponse<ProductResponseDto>();
        }

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllProductBestSeller(int bestSeller)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetAllBestSellingProducts?bestSeller={bestSeller}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
            }
            return null;
        }

        public async Task<ApiResponse<ProductResponseDto>> GetListProductAsync(int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            // Tạo URL gốc cho API
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


            // Sử dụng QueryHelpers để tạo chuỗi truy vấn
            var query = QueryHelpers.AddQueryString("/api/Product/GetListProduct", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return result;
            }

            return null;
        }

        public async Task<bool> UpdateProductStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateCateIntroDtoStatus { Status = status };
            var query = $"/api/Product/UpdateProductStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Product/DeleteProductById/{id}");

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> CreateProduct(CreateProductDto request, IBrowserFile? file, List<IBrowserFile>? selectedSecondFile)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.Name), "Name");
            content.Add(new StringContent(request.Description), "Description");
            content.Add(new StringContent(request.Detail), "Detail");
            content.Add(new StringContent(request.Price.ToString()), "Price");
            content.Add(new StringContent(request.Discount.ToString()), "Discount");
            content.Add(new StringContent(request.StockQuantity.ToString()), "StockQuantity");
            content.Add(new StringContent(request.Rating.ToString()), "Rating");
            content.Add(new StringContent(request.IsHot.ToString()), "IsHot");
            content.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");

            // Thêm ảnh chính
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "ImageFile", file.Name);
            }

            // Thêm các ảnh phụ
            if (selectedSecondFile != null)
            {
                int index = 0;
                foreach (var secondFile in selectedSecondFile)
                {
                    //var fileSecondContent = new StreamContent(secondFile.OpenReadStream(10485760));
                    //fileSecondContent.Headers.ContentType = new MediaTypeHeaderValue(secondFile.ContentType);
                    //content.Add(fileSecondContent, "ImageObjectList", secondFile.Name);
                    var fileSecondContent = new StreamContent(secondFile.OpenReadStream(10485760)); // 10MB limit
                    fileSecondContent.Headers.ContentType = new MediaTypeHeaderValue(secondFile.ContentType);
                    content.Add(fileSecondContent, $"ImageObjectList[{index}]", secondFile.Name ?? $"file_{index}");
                    index++;
                }
            }

            // Gửi request
            var response = await _httpClient.PostAsync("/api/Product/CreateProduct", content);
            return response.IsSuccessStatusCode;
        }



    }
}
