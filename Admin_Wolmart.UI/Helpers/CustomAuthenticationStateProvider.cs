using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTO.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Admin_Wolmart.UI.Helpers
{
    public class CustomAuthenticationStateProvider(CookieService cookieService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private const string TokenKey = "AuthToken";

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = cookieService.GetCookie(TokenKey);

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(anonymous);
            }

            var userClaims = DecryptToken(token);
            if (userClaims == null)
            {
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            var claimsPrincipal = SetClaimPrincipal(userClaims);
            return Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public Task UpdateAuthenticationState(AuthResponseDto userSession)
        {
            ClaimsPrincipal claimsPrincipal = anonymous;

            if (!string.IsNullOrEmpty(userSession.Token))
            {
                var userClaims = DecryptToken(userSession.Token);
                if (userClaims != null)
                {
                    cookieService.SetCookie(TokenKey, userSession.Token, 120); // Expiry set to 2 hours
                    claimsPrincipal = SetClaimPrincipal(userClaims);

                    userSession.UserId = userClaims.Id!;
                    userSession.Role = userClaims.Role!;
                }
                else
                {
                    cookieService.DeleteCookie(TokenKey);
                }
            }
            else
            {
                cookieService.DeleteCookie(TokenKey);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                new(ClaimTypes.NameIdentifier, claims.Id!),
                new(ClaimTypes.Name, claims.Name!),
                new(ClaimTypes.Email, claims.Email!),
                new(ClaimTypes.Role, claims.Role!),
                }, "JwtAuth"));
        }

        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(jwtToken)) return new CustomUserClaims();

            var token = handler.ReadJwtToken(jwtToken);

            // Lấy các claims và kiểm tra null
            var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            return new CustomUserClaims(userId, name, email, role);
        }

    }
}