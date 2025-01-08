using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Admin_Wolmart.UI.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            // Kiểm tra cookie token
            var token = httpContext.Request.Cookies["accesstoken_admin"];
            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            // Đọc và kiểm tra token
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            var identity = new ClaimsIdentity(jwtToken.Claims, "accesstoken_admin");
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwtToken.Claims, "accesstoken_admin");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

    }
}

