using Microsoft.AspNetCore.Components.Forms;
using Shared.DTO.Banner;
using Shared.DTO.Payment;
using Shared.DTO.Response;
using System.Net.Http.Headers;

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

        public async Task<bool> CreatePaymentAsync(CreatePaymentDto request, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            // Thêm các trường khác vào form-data
            content.Add(new StringContent(request.PaymentType), "PaymentType");
            content.Add(new StringContent(request.BankName), "BankName");
            content.Add(new StringContent(request.AccountNumber), "AccountNumber");
            content.Add(new StringContent(request.Note), "Note");

            // Đọc và thêm file vào form-data
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(10485760)); // 10MB limit
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "File", file.Name);
            }

            // Gửi request
            var response = await _httpClient.PostAsync("/api/PaymentMethod/CreatePaymentMethod", content);
            return response.IsSuccessStatusCode;
        }
    }
}
