using Shared.DTO.Response;
using Shared.DTO.ShippingCost;

namespace Ecommerce.UI.Services
{
    public class ShippingCost
    {
        private readonly HttpClient _httpClient;

        public ShippingCost(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<ShippingCostDto>> GetShippingCostByAddressIdAsync(Guid addressId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<ShippingCostDto>>($"/api/ShippingCost/GetShippingCostByAddressId/{addressId}");
            return response;
        }
    }
}
