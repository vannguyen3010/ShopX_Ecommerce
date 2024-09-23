using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.JwtFeatures;
using EmailService;
using Entities.Identity;
using Entities.Models.Address;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Response;
using Shared.DTO.User;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;

        public AccountsController(UserManager<User> userManager, IRepositoryManager repository, IMapper mapper, JwtHandler jwtHandler, IEmailSender emailSender)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password!);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegisterResponseDto { Errors = errors });
            }
            await _userManager.AddToRoleAsync(user, "User");

            //Tạo Token xác thực 
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //Send the email
            var message = new Message(new string[] { user.Email! }, "Confirm your email", $"Your email confirmation token is: {token}", null!);

            await _emailSender.SendEmailAsync(message);

            return StatusCode(201);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {

            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Kiểm tra Email và Mật khẩu!",
                    Data = null
                });
            }

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

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Sai Email hoặc mật khẩu. Vui lòng kiểm tra lại.",
                    Data = null
                });
            }

            if (user.EmailConfirmed == false)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Email cần phải xác nhận.",
                    Data = null
                });
            }

            var token = await _jwtHandler.GenerateToken(user);

            await _userManager.ResetAccessFailedCountAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
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
            var message = new Message(new string[] { user.Email! }, "token", $"Reset password token: {token}", null!);

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


            //Đặt Lại Mật Khẩu
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(x => x.Description);

                return BadRequest(new { Errors = errors });
            }
            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));

            //Nếu việc đặt lại mật khẩu thành công, thiết lập ngày khóa tài khoản của người dùng thành một ngày trong quá khứ để mở khóa tài khoản nếu nó bị khóa trước đó.
            return Ok();
        }

        [HttpGet]
        [Route("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return Ok();
        }

        [HttpGet]
        [Route("GetAllUsers")]
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
                return Ok(userDtos);
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
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

                return Ok(userDto);
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
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
                    return NoContent();
                }

                return BadRequest(result.Errors);
            }
            catch (Exception)
            {

                return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.");
            }
        }

    }
}
