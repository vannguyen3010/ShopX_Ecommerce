namespace Admin_Wolmart.UI.Helpers
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SetCookie(string key, string value, int expiryInMinutes)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.Now.AddMinutes(expiryInMinutes)
                };
                httpContext.Response.Cookies.Append(key, value, cookieOptions);
            }
        }

        public string? GetCookie(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return httpContext?.Request.Cookies[key];
        }

        public void DeleteCookie(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                httpContext.Response.Cookies.Delete(key);
            }
        }
    }
}
