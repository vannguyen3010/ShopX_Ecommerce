namespace Admin_Wolmart.UI.Helpers
{
    public class CookieService(IHttpContextAccessor httpContextAccessor)
    {

        public void SetCookie(string key, string value, int expiryInMinutes)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.Now.AddMinutes(expiryInMinutes)
            };

            httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, cookieOptions);
        }

        public string? GetCookie(string key)
        {
            return httpContextAccessor.HttpContext?.Request.Cookies[key];
        }

        public void DeleteCookie(string key)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
        }
    }
}
