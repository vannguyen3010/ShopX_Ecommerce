namespace Admin_Wolmart.UI.Helpers
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void Set(string key, string value, CookieOptions options)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
        }

        public string? Get(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return httpContext?.Request.Cookies[key];
        }

        public void Delete(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                httpContext.Response.Cookies.Delete(key);
            }
        }
    }
}

