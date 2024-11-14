using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTO.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Admin_Wolmart.UI.Helpers
{
    public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private bool _initialized = false;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Nếu thành phần chưa được khởi tạo, hãy trả về trạng thái ẩn danh.
            if (!_initialized)
            {
                return new AuthenticationState(anonymous);
            }

            // Lấy token từ LocalStorage
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return new AuthenticationState(anonymous);

            var deserializeToken = Serializations.DeserializeJsonString<AuthResponseDto>(stringToken);
            if (deserializeToken == null) return new AuthenticationState(anonymous);

            var getUserClaims = DecryptToken(deserializeToken.Token!);
            if (getUserClaims == null) return new AuthenticationState(anonymous);

            var claimsPrincipal = SetClaimPrincipal(getUserClaims);
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task UpdateAuthenticationState(AuthResponseDto userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (userSession.Token != null || userSession.RefreshTokens != null)
            {

                userSession.IsAuthSuccessful = true;

                var serializeSession = Serializations.SerializeObj(userSession);//Serialize đối tượng userSession thành chuỗi JSON.
                await localStorageService.SetToken(serializeSession);//Lưu trữ chuỗi JSON này vào LocalStorage.


                var getUserClaims = DecryptToken(userSession.Token!); //Giải mã token để lấy thông tin người dùng.
                claimsPrincipal = SetClaimPrincipal(getUserClaims);//Tạo ClaimsPrincipal từ các thông tin người dùng giải mã được.

                userSession.UserId = getUserClaims.Id!;
                userSession.Role = getUserClaims.Role!;

                var updatedSession = Serializations.SerializeObj(userSession);
                await localStorageService.SetToken(updatedSession);

                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await localStorageService.RemoveToken();//Nếu không có token hoặc refresh token, xóa token khỏi LocalStorage.
            }
            _initialized = true;
            //Gửi thông báo rằng trạng thái xác thực đã thay đổi, cập nhật trạng thái xác thực với claimsPrincipal mới.
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        // Helper method to create ClaimsPrincipal from decrypted token information
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
            var token = handler.ReadJwtToken(jwtToken);

            // Lấy các claims và kiểm tra null
            var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            // Xử lý trường hợp thiếu thông tin bắt buộc
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userId))
                throw new Exception("Required claims (Email or NameIdentifier) are missing in the token.");

            return new CustomUserClaims(userId, name, email, role);
        }

    }

}