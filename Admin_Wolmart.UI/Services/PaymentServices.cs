using Shared.DTO.CategoryIntroduce;
using Shared.DTO.Payment;
using Shared.DTO.Response;

namespace Admin_Wolmart.UI.Services
{
    public class PaymentServices
    {
        private readonly HttpClient _httpClient;

        public PaymentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<PaymentMethodDto>>> GetAllPaymentMethodAsync()
        {
            var response = await _httpClient.GetAsync("/api/PaymentMethod/GetAllPaymentMethod");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<PaymentMethodDto>>>();
            }
            return null;
        }

        public async Task<bool> UpdatePaymentStatusAsync(Guid id, bool status)
        {
            var updateStatus = new UpdatePaymentStatusDto { Status = status };
            var query = $"/api/PaymentMethod/UpdatePaymentMethodStatus/{id}?Status={status}";
            var response = await _httpClient.PutAsJsonAsync(query, updateStatus);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePaymentAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/PaymentMethod/DeletePaymentMethod/{id}");

            return response.IsSuccessStatusCode;

        }
    }
}
