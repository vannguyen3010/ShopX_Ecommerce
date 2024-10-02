using Shared;
using Shared.DTO.Banner;
using Shared.DTO.Response;

namespace Ecommerce.UI.Services
{
    public class BannerServices
    {
        private readonly HttpClient _httpClient;

        public BannerServices(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

      
    }
}
