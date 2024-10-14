using Shared.DTO.Cart;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class CartServices
    {
        private readonly HttpClient _httpClient;

        public CartServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<CartItemDto>> AddToCartAsync(AddToCartDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Cart/AddToCart", request);

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<CartItemDto>>();
            }
            else
            {
                throw new HttpRequestException("Error when adding item to cart.");
            }
        }

        public async Task<ApiResponse<CartDtos>> GetCartAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/Cart/GetCart/{userId}");

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CartDtos>>();
                return result;
            }
            return null;
        }

        public async Task<ApiResponse<CartItemDto>> UpdateCartItemQuantityAsync(UpdateCartItemDto request)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/cart/UpdateCartItemQuantity", request);

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<CartItemDto>>();
            }
            return new ApiResponse<CartItemDto>
            {
                Success = false,
                Message = "Failed to update cart item quantity"
            };
        }

        public async Task<ApiResponse<object>> DeleteFromCartAsync(string userId, Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Cart/DeleteFromCart/{productId}?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return result;
            }
            return new ApiResponse<object>
            {
                Success = false,
                Message = "Error deleting item from cart."
            };
        }
    }
}
