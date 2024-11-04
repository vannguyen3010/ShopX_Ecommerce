using Microsoft.AspNetCore.WebUtilities;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Contact;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class ContactServices
    {
        private readonly HttpClient _httpClient;

        public ContactServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<ContactResponse>> GetAllContactAsync(int pageNumber, int pageSize, int? type = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                ["pageNumber"] = pageNumber.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            if (type.HasValue)
            {
                queryParameters["type"] = type.Value.ToString();
            }

            var query = QueryHelpers.AddQueryString("/api/Contact/GetAllContact", queryParameters);

            var response = await _httpClient.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<ContactResponse>>();
                return result;
            }
            return null;

        }

        public async Task<bool> UpdateContactStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdateContactDto { Status = status };
            var query = $"/api/Contact/UpdateContact/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResponse<ContactDto>> GetContactByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Contact/GetContactById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<ApiResponse<ContactDto>>();
                return category;
            }
            return null;
        }
    }
}
