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
