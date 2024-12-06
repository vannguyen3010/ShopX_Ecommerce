using AutoMapper;
using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.ImageProfile;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(UserManager<User> userManager, ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("CreateProfileUser")]
        [Authorize]
        public async Task<IActionResult> CreateProfileUser([FromForm] CreateProfileUserDto request)
        {
            try
            {
                ValidateFileUpload(request);

                if (request == null)
                {
                    _logger.LogError("No file uploaded");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"No file uploaded",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Banner object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                //Kiểm tra người dùng có tồn tại không
                var userExits = await _repository.CommentProduct.GetUserByIdAsync(request.UserId);

                if (userExits == null)
                {
                    _logger.LogError($"User with id {request.UserId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"User with id {request.UserId} not found.",
                        Data = null
                    });
                }

                var existingProfile = await _repository.Profile.GetProfileByUserIdAsync(request.UserId, trackChanges: false);
                if (existingProfile != null)
                {
                    _logger.LogError($"User with id {request.UserId} already has a profile.");
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"User with id {request.UserId} already has a profile.",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var profileEntity = _mapper.Map<ProfileUser>(request);

                // Xử lý tập tin hình ảnh
                if (request.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
                    var fileExtension = Path.GetExtension(request.File.FileName);
                    profileEntity.FilePath = await SaveFileAndGetUrl(request.File, fileName, fileExtension);
                    profileEntity.FileName = fileName;
                    profileEntity.FileExtension = fileExtension;
                    profileEntity.FileSizeInBytes = request.File.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.Profile.CreateProfileUserAsync(profileEntity);

                var profileUserDto = _mapper.Map<ProfileUserDto>(profileEntity);

                return Ok(new ApiResponse<ProfileUserDto>
                {
                    Success = true,
                    Message = "profile retrieved successfully.",
                    Data = profileUserDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ProfileImage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetProfileByUserId/{userId}")]
        public async Task<IActionResult> GetProfileByUserId(string userId)
        {
            try
            {
                var profile = await _repository.Profile.GetProfileByUserIdAsync(userId, trackChanges: false);
                if (profile == null)
                {
                    _logger.LogError($"Không tìm thấy người dùng id này {userId}");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy người dùng {userId} này!",
                        Data = null
                    });
                }

                var profileDto = _mapper.Map<ProfileUserDto>(profile);

                return Ok(new ApiResponse<ProfileUserDto>
                {
                    Success = true,
                    Message = "Kết quả thành công.",
                    Data = profileDto
                });

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ProfileImage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteProfile/{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            try
            {
                var profile = await _repository.Profile.GetProfileByIdAsync(id, trackChanges: false);
                if (profile == null)
                {
                    _logger.LogError($"Address with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Profile.DeleteProfileAsync(profile);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Profile action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateProfile/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromForm] UpdateProFileDto request)
        {
            try
            {
                UpdateFileUpload(request);

                if (request == null)
                {
                    _logger.LogError("ImageProfile object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy id ImageProfile này!",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ImageProfile object sent from client.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid ImageProfile object sent from client!",
                        Data = null
                    });
                }

                var profileEntity = await _repository.Profile.GetProfileByIdAsync(id, trackChanges: true);
                if (profileEntity == null)
                {
                    _logger.LogError($"ImageProfile with id: {id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "ImageProfile with id: {id}, hasn't been found in db!",
                        Data = null
                    });
                }

                // Ánh xạ từ request sang profileEntity (các trường khác ngoài ảnh)
                _mapper.Map(request, profileEntity);

                if (request.File != null)
                {
                    profileEntity.FileExtension = Path.GetExtension(request.File.FileName);
                    profileEntity.FileSizeInBytes = request.File.Length;
                    profileEntity.FileName = request.File.FileName;
                    profileEntity.FilePath = await SaveFileAndGetUrl(request.File, profileEntity.FileName, profileEntity.FileExtension);
                }

                await _repository.Profile.UpdateProfileUserAsync(profileEntity);

                return Ok(new ApiResponse<ProfileUserDto>
                {
                    Success = true,
                    Message = "Profile updated successfully.",
                    Data = _mapper.Map<ProfileUserDto>(profileEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ProfileImage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void UpdateFileUpload(UpdateProFileDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.File != null)
            {
                //// Kiểm tra phần mở rộng tệp
                if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
                {
                    ModelState.AddModelError("File", "Unsupported file extension");
                }

                //// Kiểm tra kích thước tệp
                if (request.File.Length > 10485760)// Tệp lớn hơn 10MB
                {
                    ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
                }
            }

        }
        private void ValidateFileUpload(CreateProfileUserDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.File != null)
            {
                //// Kiểm tra phần mở rộng tệp
                if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
                {
                    ModelState.AddModelError("File", "Unsupported file extension");
                }

                //// Kiểm tra kích thước tệp
                if (request.File.Length > 10485760)// Tệp lớn hơn 10MB
                {
                    ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
                }
            }

        }
        private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/img-Profile", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/img-Profile/{fileName}{fileExtension}";

            return urlFilePath;
        }
    }
}
