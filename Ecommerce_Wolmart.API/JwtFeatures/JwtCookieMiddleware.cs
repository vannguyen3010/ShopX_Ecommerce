namespace Ecommerce_Wolmart.API.JwtFeatures
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Kiểm tra nếu Authorization Header chưa có và cookie chứa "access_token"
            if (!context.Request.Headers.ContainsKey("Authorization") && context.Request.Cookies.ContainsKey("access_token"))
            {
                var token = context.Request.Cookies["access_token"];
                if (!string.IsNullOrWhiteSpace(token))
                {
                    // Thêm Bearer Token vào Authorization Header
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }

            await _next(context);
        }
    }
}
