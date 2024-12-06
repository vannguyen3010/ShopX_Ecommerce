using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTO.User;
using System.Net.Http;

namespace LoginAuthen.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(LoginDto loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginAdmin", loginModel);

            //Store Token
            await _localStorage.SetItemAsync("accessToken", response.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Accounts/LoginAdmin", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                if (result.IsAuthSuccessful)
                {
                    var cookieOptions = new CookieOptions { HttpOnly = true, Secure = true };

                    _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", result.Token, cookieOptions);

                    return new AuthResponseDto
                    {
                        IsAuthSuccessful = true,
                    };
                }
            }

            return new AuthResponseDto
            {
                IsAuthSuccessful = false,
            };
        }

    }
}
