using Microsoft.AspNetCore.WebUtilities;
using Shared;
using Shared.DTO.BannerProduct;
using Shared.DTO.CateProduct;
using Shared.DTO.CommentProduct;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class ProductServices
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<BannerProductDto>> GetAllBannerPositionProductPopup(BannerProductWithPopupPosition position)
        {
            var response = await _httpClient.GetAsync($"/api/BannerProduct/GetAllBannerPositionProductPopup?position={position}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<BannerProductDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data;
            }
            return new List<BannerProductDto>();
        }

        public async Task<ApiProductResponse<ProductDto, IEnumerable<ProductDto>>> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync($"api/Product/GetProductById/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiProductResponse<ProductDto, IEnumerable<ProductDto>>>();
            }
            return null; // Hoặc xử lý lỗi ở đây
        }

        public async Task<ApiResponse<IEnumerable<CommentProductDto>>> GetAllCommentProductByProductId(Guid productid)
        {
            var response = await _httpClient.GetAsync($"/api/CommentProduct/GetAllCommentProductByProductId/{productid}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<CommentProductDto>>>();
                return data;
            }
            return null;
        }

        public async Task<bool> CreateCommentProduct(CreateCommentProductDto commentDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/CommentProduct/CreateCommentProduct", commentDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResponse<ProductResponseDto>> GetListProductAsync(int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            // Tạo URL gốc cho API
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            // Thêm các tham số tùy chọn nếu có giá trị
            if (minPrice.HasValue)
            {
                queryParameters["minPrice"] = minPrice.Value.ToString();
            }

            if (maxPrice.HasValue)
            {
                queryParameters["maxPrice"] = maxPrice.Value.ToString();
            }

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

        public async Task<IEnumerable<ProductDto>> GetAllProductIsHotAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/GetAllProductIsHot");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
                return apiResponse?.Data ?? Enumerable.Empty<ProductDto>();
            }
            else
            {
                return Enumerable.Empty<ProductDto>();
            }
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

        public async Task<ApiResponse<CateProductDto>> GetCategoryProductByCategoryId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/CateProduct/GetCategoryProductByCategoryId/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<CateProductDto>>();
            }
            return null;
        }
    }
}
