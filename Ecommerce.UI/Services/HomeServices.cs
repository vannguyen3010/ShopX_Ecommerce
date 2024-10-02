﻿using Shared.DTO.Banner;
using Shared;
using Shared.DTO.Product;
using Shared.DTO.Response;
using System.Net.Http;

namespace Ecommerce.UI.Services
{
    public class HomeServices
    {
        private readonly HttpClient _httpClient;

        public HomeServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BannerDto>> GetAllBannersAsync(BannerPosition position)
        {
            var response = await _httpClient.GetAsync($"/api/Banner/GetAllBannerPosition?position={position}");
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<BannerDto>>>();
            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data;
            }
            return new List<BannerDto>();
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
        public async Task<IEnumerable<ProductDto>> GetAllProductDiscountAsync()
        {
            var response = await _httpClient.GetAsync("/api/Product/GetAllProductDiscount");
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

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllNewProductsAsync(DateTime? startDate = null)
        {
            var query = startDate.HasValue ? $"?startDate={startDate.Value.ToString("yyyy-MM-dd")}" : string.Empty;
            var response = await _httpClient.GetAsync($"/api/Product/GetAllNewProducts{query}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<ProductDto>>>();
                return content!;
            }
            else
            {
                throw new Exception("Không thể lấy danh sách sản phẩm mới.");
            }
        }
    }
}