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

    }
}
