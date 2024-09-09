using AutoMapper;
using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.CateProduct;
using Shared.DTO.ImageProfile;
using Shared.DTO.Product;
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
        [Route("create-imageProfile")]
        public async Task<IActionResult> CreateImageProfile([FromForm] CreateImagePrifileDto createImageDto)
        {
            try
            {
                ValidateFileUpload(createImageDto);

                if (createImageDto == null)
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
                var userExits = await _repository.CommentProduct.GetUserByIdAsync(createImageDto.UserId);

                if (userExits == null)
                {
                    _logger.LogError($"User with id {createImageDto.UserId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"User with id {createImageDto.UserId} not found.",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var imageEntity = _mapper.Map<Image>(createImageDto);

                // Xử lý tập tin hình ảnh
                if (createImageDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createImageDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createImageDto.File.FileName);
                    imageEntity.FilePath = await SaveFileAndGetUrl(createImageDto.File, fileName, fileExtension);
                    imageEntity.FileName = fileName;
                    imageEntity.FileExtension = fileExtension;
                    imageEntity.FileSizeInBytes = createImageDto.File.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.Profile.CreateImageProfileAsync(imageEntity);

                var imageProfileDto = _mapper.Map<ImageProfileDto>(imageEntity);

                imageProfileDto.UserId = Guid.Parse(createImageDto.UserId);

                return Ok(new ApiResponse<ImageProfileDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = imageProfileDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ProfileImage action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetProfileUserByUserId/{userId}")]
        public async Task<IActionResult> GetProfileUserByUserId(string userId)
        {
            try
            {
                var profileUser = await _repository.Profile.GetProfileByUserIdAsync(userId);
                if (profileUser == null)
                {
                    _logger.LogError("Profile not found");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Profile not found",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<ProfileDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<ProfileDto>(profileUser)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ProfileUSer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void ValidateFileUpload(CreateImagePrifileDto request)
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
