using Shared.DTO.Banner;
using Shared;
using Shared.DTO.Product;
using Shared.DTO.Response;
using Shared.DTO.BannerProduct;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Shared.DTO.CommentProduct;
using System.Text;
using System;

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

        public async Task<ApiResponses> GetProductsByCategoryIdAsync(Guid categoryId, int pageNumber , int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetProductsByCategoryId/{categoryId}?pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponses>();
                return data!;
            }
            return null;
        }

        public async Task<ApiResponse<ProductResponseDto>> GetAllProductsByPrice(decimal minPrice, decimal? maxPrice, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Product/GetAllProductsByPrice?=min?minPrice={minPrice}&maxPrice={maxPrice}&pageNumber={pageNumber}&pageSize={pageSize}");
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return result;
            }
            return new ApiResponse<ProductResponseDto>
            {
                Success = false,
                Message = "Không thể tải sản phẩm."
            };
        }
        
        public async Task<ApiResponse<ProductResponseDto>> GetListProductAsync(int pageNumber = 1, int pageSize = 10, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null)
        {
            var query = $"/api/Product/GetListProduct?pageNumber={pageNumber}&pageSize={pageSize}";

            if (minPrice.HasValue)
            {
                query += $"&minPrice={minPrice.Value}";
            }
            if (maxPrice.HasValue)
            {
                query += $"&maxPrice={maxPrice.Value}";
            }
            if (categoryId.HasValue)
            {
                query += $"&categoryId={categoryId.Value}";
            }

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDto>>();
                return result;
            }
            return null;
        }
    }
}
