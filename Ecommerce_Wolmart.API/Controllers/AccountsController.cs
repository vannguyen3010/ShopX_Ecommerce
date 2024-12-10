using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.JwtFeatures;
using EmailService;
using Entities.Identity;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.DTO.Address;
using Shared.DTO.Response;
using Shared.DTO.User;
using System.Security.Cryptography;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;
        private readonly ILoggerManager _logger;

        public AccountsController(UserManager<User> userManager, IConfiguration configuration, IRepositoryManager repository, IMapper mapper, JwtHandler jwtHandler, IEmailSender emailSender, ILoggerManager logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;
            _logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null || !ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Dữ liệu đăng ký không hợp lệ!",
                    Data = null
                });
            }

            // Kiểm tra email đã tồn tại
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email!);
            if (existingUser != null)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Email đã được đăng ký vui lòng nhập email khác!",
                    Data = null
                });
            }

            var user = _mapper.Map<User>(registerDto);


            var result = await _userManager.CreateAsync(user, registerDto.Password!);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegisterResponseDto { Errors = errors });
            }

            //   // Thêm người dùng vào vai trò "User"
            await _userManager.AddToRoleAsync(user, "User");


            // Tạo mã xác nhận ngẫu nhiên
            var verificationCode = new Random().Next(100000, 999999).ToString();// Mã 6 chữ số ngẫu nhiên

            // Lưu mã xác nhận vào thuộc tính của người
            user.VerificationCode = verificationCode;
            await _userManager.UpdateAsync(user);

            // Gửi mã xác nhận qua email
            var message = new Message(
                new string[] { user.Email! },
                "Confirm your email",
                $"Mã xác thực: {verificationCode}",
                null!
            );

            await _emailSender.SendEmailAsync(message);

            return StatusCode(201);
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            // Kiểm tra Email và Mật khẩu
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Kiểm tra Email và Mật khẩu!",
                    Data = null
                });
            }

            // Tìm người dùng theo email
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);
            if (user == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Email chưa đăng ký!",
                    Data = null
                });
            }

            // Kiểm tra xem người dùng có phải là User, Admin hoặc SuperAdmin không
            bool isUser = await _userManager.IsInRoleAsync(user, RoleEnum.User.ToString());

            // Kiểm tra xem người dùng có phải là User không
            if (!isUser)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Tài khoản này không có quyền truy cập.",
                    Data = null
                });
            }

            // Kiểm tra mật khẩu
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Sai Email hoặc mật khẩu. Vui lòng kiểm tra lại.",
                    Data = null
                });
            }

            // Kiểm tra xác nhận email
            if (user.EmailConfirmed == false)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Email cần phải xác nhận.",
                    Data = null
                });
            }

            // Tạo JWT Token
            var token = await _jwtHandler.GenerateToken(user);


            // Kiểm tra và xử lý Refresh Token
            var refreshToken = await _repository.AccountRepository.GetByUserIdAsync(user.Id);
            if (refreshToken == null)
            {
                // Nếu chưa có Refresh Token, tạo mới
                refreshToken = new RefreshToken
                {
                    RefreshTokens = GenerateRefreshToken(),
                    Expiration = DateTime.UtcNow.AddDays(7),
                    IsUsed = false,
                    IsRevoked = false,
                    UserId = user.Id
                };

                // Thêm Refresh Token vào cơ sở dữ liệu
                await _repository.AccountRepository.AddAsync(refreshToken);
            }
            else
            {
                // Nếu đã có Refresh Token, cập nhật lại thông tin
                refreshToken.RefreshTokens = GenerateRefreshToken();
                refreshToken.Expiration = DateTime.UtcNow.AddDays(7);
                refreshToken.IsUsed = false;

                // Cập nhật Refresh Token trong cơ sở dữ liệu
                await _repository.AccountRepository.UpdateAsync(refreshToken);
            }

            HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            // Đặt lại số lần đăng nhập không thành công
            await _userManager.ResetAccessFailedCountAsync(user);


            // Trả về kết quả
            return Ok(new RefreshTokenResponseDto
            {
                IsAuthSuccessful = true,
                Token = token,
                RefreshTokens = refreshToken.RefreshTokens,
                UserId = user.Id
            });
        }

        [HttpPost]
        [Route("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginDto loginDto)
        {
            // Kiểm tra Email và Mật khẩu
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Kiểm tra Email và Mật khẩu!",
                    Data = null
                });
            }

            // Tìm người dùng theo email
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Thông tin đăng nhập không hợp lệ.",
                    Data = null
                });
            }

            // Kiểm tra xem người dùng có phải là Admin hoặc SuperAdmin không
            bool isAdmin = await _userManager.IsInRoleAsync(user, RoleEnum.Admin.ToString());
            bool isSuperAdmin = await _userManager.IsInRoleAsync(user, RoleEnum.SuperAdmin.ToString());

            // Kiểm tra xem người dùng có phải là User không
            if (!isAdmin && !isSuperAdmin)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Tài khoản này không có quyền truy cập.",
                    Data = null
                });
            }

            // Kiểm tra mật khẩu
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Sai Email hoặc mật khẩu. Vui lòng kiểm tra lại.",
                    Data = null
                });
            }

            // Kiểm tra xác nhận email
            if (user.EmailConfirmed == false)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Email cần phải xác nhận.",
                    Data = null
                });
            }

            // Tạo JWT Token
            var token = await _jwtHandler.GenerateToken(user);

            // Kiểm tra và xử lý Refresh Token
            var refreshToken = await _repository.AccountRepository.GetByUserIdAsync(user.Id);
            if (refreshToken == null)
            {
                // Nếu chưa có Refresh Token, tạo mới
                refreshToken = new RefreshToken
                {
                    RefreshTokens = GenerateRefreshToken(),
                    Expiration = DateTime.UtcNow.AddDays(7),
                    IsUsed = false,
                    IsRevoked = false,
                    UserId = user.Id
                };

                // Thêm Refresh Token vào cơ sở dữ liệu
                await _repository.AccountRepository.AddAsync(refreshToken);
            }
            else
            {
                // Nếu đã có Refresh Token, cập nhật lại thông tin
                refreshToken.RefreshTokens = GenerateRefreshToken();
                refreshToken.Expiration = DateTime.UtcNow.AddDays(7);
                refreshToken.IsUsed = false;

                // Cập nhật Refresh Token trong cơ sở dữ liệu
                await _repository.AccountRepository.UpdateAsync(refreshToken);
            }

            // Lưu token vào cookie
            HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            //HttpContext.Response.Cookies.Append("refresh_token", refreshToken.RefreshTokens, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = refreshToken.Expiration
            //});

            // Đặt lại số lần đăng nhập không thành công
            await _userManager.ResetAccessFailedCountAsync(user);

            // Trả về kết quả
            return Ok(new AuthResponseDto
            {
                IsAuthSuccessful = true,
                Token = token,
                RefreshTokens = refreshToken.RefreshTokens,
                UserId = user.Id
            });
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            // Xóa token trong cookie bằng cách đặt giá trị cookie rỗng và hết hạn
            Response.Cookies.Append("access_token", string.Empty, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Cookie hết hạn
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict // Bảo mật cookie
            });

            return Ok(new ApiResponse<Object>
            {
                Success = true,
                Message = "Đăng xuất thành công.",
                Data = null
            });
        }



        [HttpPost]
        [Route("RefreshToken")]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (string.IsNullOrEmpty(refreshTokenDto.RefreshTokens))
            {
                return BadRequest(new ApiResponse<Object>
                {
                    Success = false,
                    Message = "Refresh token không hợp lệ.",
                    Data = null
                });
            }

            // Kiểm tra refresh token
            var existingRefreshToken = await _repository.AccountRepository.GetByTokenAsync(refreshTokenDto.RefreshTokens);
            if (existingRefreshToken == null || existingRefreshToken.IsUsed || existingRefreshToken.Expiration < DateTime.UtcNow)
            {
                return Unauthorized(new ApiResponse<Object>
                {
                    Success = false,
                    Message = "Refresh token không hợp lệ hoặc đã hết hạn. Vui lòng đăng nhập lại.",
                    Data = null
                });
            }

            // Lấy user dựa trên refresh token
            var user = await _userManager.FindByIdAsync(existingRefreshToken.UserId);
            if (user == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = "Người dùng không tồn tại.",
                    Data = null
                });
            }

            // Tạo Token và Refresh Token mới
            var newAccessToken = await _jwtHandler.GenerateToken(user);

            // Đánh dấu refresh token là đã sử dụng
            existingRefreshToken.IsUsed = true;
            existingRefreshToken.IsRevoked = true; // Đảm bảo không thể tái sử dụng

            await _repository.AccountRepository.UpdateAsync(existingRefreshToken);

            return Ok(new RefreshTokenResponseDto
            {
                IsAuthSuccessful = true,
                Token = newAccessToken,
                RefreshTokens = refreshTokenDto.RefreshTokens
            });

        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email!);
            if (user == null)
                return BadRequest("Invalid Request");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            //var param = new Dictionary<string, string>
            //{
            //    { "token", token },
            //    { "email", forgotPasswordDto.Email! }
            //};

            //var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURL!,param!);
            //var message = new Message(new string[] { user.Email! }, "Reset password token", callback, null!);

            //Send the email
            //var message = new Message(new string[] { user.Email! }, "token", $"Reset password token: {token}", null!);
            //await _emailSender.SendEmailAsync(message);

            // Tạo mã xác nhận ngẫu nhiên
            var verificationCode = new Random().Next(100000, 999999).ToString();// Mã 6 chữ số ngẫu nhiên

            // Lưu mã xác nhận vào thuộc tính của người
            user.VerificationCode = verificationCode;
            await _userManager.UpdateAsync(user);

            // Gửi mã xác nhận qua email
            var message = new Message(
                new string[] { user.Email! },
                "Confirm your email",
                $"Mã xác thực: {verificationCode}",
                null!
            );

            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email!);
            if (user == null)
                return BadRequest("Invalid Request");

            // Kiểm tra mã xác nhận (verification code)
            if (user.VerificationCode != resetPasswordDto.VerificationCode)
            {
                return BadRequest("Mã xác nhận không đúng.");
            }

            // Đặt lại mật khẩu nếu mã xác nhận hợp lệ
            var resetPassResult = await _userManager.RemovePasswordAsync(user);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(x => x.Description);
                return BadRequest(new { Errors = errors });
            }

            // Đặt mật khẩu mới
            var addPassResult = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password!);
            if (!addPassResult.Succeeded)
            {
                var errors = addPassResult.Errors.Select(x => x.Description);
                return BadRequest(new { Errors = errors });
            }

            // Xóa mã xác nhận sau khi đặt lại mật khẩu thành công
            user.VerificationCode = null;
            await _userManager.UpdateAsync(user);

            // Reset trạng thái khóa tài khoản
            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));

            return Ok("Mật khẩu đã được đặt lại thành công.");
        }

        [HttpGet]
        [Route("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string verificationCode)
        {
            // Tìm người dùng dựa trên email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            // Kiểm tra mã xác thực (verificationCode)
            if(user.VerificationCode != verificationCode)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Mã xác minh không hợp lệ!",
                    Data = null
                });
            }

            // Cập nhật trạng thái xác nhận email của người dùng
            user.EmailConfirmed = true;
            user.VerificationCode = null; // Xóa mã xác nhận sau khi xác nhận thành công
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPost]
        [Route("ChangePassWord")]
        public async Task<IActionResult> ChangePassWord([FromBody] ChangePasswordDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid password change request.");
                return BadRequest(ModelState);
            }

            // Tìm người dùng dựa trên UserId
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogError($"Không tìm thấy người dùng này {request.UserId}");
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Không tìm thấy người dùng {request.UserId} này!",
                    Data = null
                });
            }

            // Kiểm tra mật khẩu hiện tại
            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!passwordCheck)
            {
                return BadRequest("Mật khẩu hiện tại không chính xác.");
            }

            // Đổi mật khẩu mới
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                var errors = changePasswordResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok("Mật khẩu đã được thay đổi thành công.");
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Policy = "SuperAdminOrAdmin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

                foreach (var userDto in userDtos)
                {
                    var user = await _userManager.FindByIdAsync(userDto.Id!);
                    var roles = await _userManager.GetRolesAsync(user!);
                    userDto.Roles = roles;
                }
                return Ok(new ApiResponse<IEnumerable<UserDto>>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = userDtos
                });
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        [Authorize(Policy = "SuperAdminOrAdmin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy người dùng có ID {id}." });
                }

                var userDto = _mapper.Map<UserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles;

                return Ok(new ApiResponse<UserDto>
                {
                    Success = true,
                    Message = "Kết quả thành công.",
                    Data = userDto
                });
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy người dùng có ID {id}." });
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                var userDto = _mapper.Map(updateUser, user);
                var result = await _userManager.UpdateAsync(userDto);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, updateUser.Roles!);
                    return NoContent();
                }
                return BadRequest(result.Errors);
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = $"Không tìm thấy người dùng có ID {id}." });
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
