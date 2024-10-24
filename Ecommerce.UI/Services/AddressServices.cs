using Entities.Models.Address;
using Shared.DTO.Address;
using Shared.DTO.Banner;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class AddressServices
    {
        private readonly HttpClient _httpClient;

        public AddressServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AddressDto>> GetListAddressAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/Address/GetAllAddress/{userId}");

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<AddressDto>>>();
                return result?.Data ?? Enumerable.Empty<AddressDto>();
            }
            return new List<AddressDto>();
        }

        public async Task<List<Province>> GetAllProvincesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Address/GetAllProvinces");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Province>>();
        }
        public async Task<List<District>> GetAllDistrictsAsync(string provinceCode)
        {
            var response = await _httpClient.GetAsync($"/api/Address/GetAllDistricts/{provinceCode}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<District>>();
        }
        public async Task<List<Ward>> GetAllWardsAsync(string districtCode)
        {
            var response = await _httpClient.GetAsync($"/api/Address/GetAllWards/{districtCode}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Ward>>();
        }
        public async Task<bool> CreateAddressAsync(CreateAddressDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Address/CreateAddress", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<AddressDto> GetAddressByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Address/GetAddressById/{id}");

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AddressDto>();
            }

            return null;
        }

        public async Task<bool> UpdateAddressAsync(Guid id, UpdateAddressDto request)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Address/UpdateAdress/{id}", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAddressAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Address/DeleteAddress/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
