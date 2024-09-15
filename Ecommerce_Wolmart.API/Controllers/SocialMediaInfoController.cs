using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.Response;
using Shared.DTO.SocialMediaInfo;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaInfoController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SocialMediaInfoController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("CreateSocialMediaInfo")]
        public async Task<IActionResult> CreateSocialMediaInfo([FromForm] CreateSocialMediaInfoDto socialMediaInfoDto)
        {
            try
            {
                ValidateFileUpload(socialMediaInfoDto);

                if (socialMediaInfoDto == null || !ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Dữ liệu không hợp lệ.",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var socialInfoEntity = _mapper.Map<SocialMediaInfo>(socialMediaInfoDto);

                // Xử lý tập tin hình ảnh
                if (socialMediaInfoDto.LogoUrl != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(socialMediaInfoDto.LogoUrl.FileName)}";
                    var fileExtension = Path.GetExtension(socialMediaInfoDto.LogoUrl.FileName);
                    socialInfoEntity.FilePath = await SaveFileAndGetUrl(socialMediaInfoDto.LogoUrl, fileName, fileExtension);
                    socialInfoEntity.FileName = fileName;
                    socialInfoEntity.FileExtension = fileExtension;
                    socialInfoEntity.FileSizeInBytes = socialMediaInfoDto.LogoUrl.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.SocialMediaInfo.CreateSocialMediaInfoAsync(socialInfoEntity);

                return Ok(new ApiResponse<SocialMediaInfoDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<SocialMediaInfoDto>(socialInfoEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateSocialMediaInfo/{Id}")]
        public async Task<IActionResult> UpdateSocialMediaInfo(Guid Id, [FromForm] UpdateSocialMediaInfoDto socialMediaInfoDto)
        {
            UpdateFileUpload(socialMediaInfoDto);
            try
            {
                var existingInfo = await _repository.SocialMediaInfo.GetSocialMediaInfoAsync(Id, trackChanges: false);
                if (existingInfo == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy thông tin mạng xã hội với ID {Id}.",
                        Data = null
                    });
                }
                _mapper.Map(socialMediaInfoDto, existingInfo);

                if (socialMediaInfoDto.LogoUrl != null)
                {
                    existingInfo.LogoUrl = socialMediaInfoDto.LogoUrl;
                    existingInfo.FileExtension = Path.GetExtension(socialMediaInfoDto.LogoUrl.FileName);
                    existingInfo.FileSizeInBytes = socialMediaInfoDto.LogoUrl.Length;
                    existingInfo.FileName = socialMediaInfoDto.LogoUrl.FileName;
                    existingInfo.FileDescription = socialMediaInfoDto.FileDescription;
                    existingInfo.FilePath = await SaveFileAndGetUrl(socialMediaInfoDto.LogoUrl, existingInfo.FileName, existingInfo.FileExtension);
                }

                await _repository.SocialMediaInfo.UpdateSocialMediaInfoAsync(existingInfo);
                return Ok(_mapper.Map<SocialMediaInfoDto>(existingInfo));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void UpdateFileUpload(UpdateSocialMediaInfoDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.LogoUrl != null)
            {
                //// Kiểm tra phần mở rộng tệp
                if (allowedExtensions.Contains(Path.GetExtension(request.LogoUrl.FileName)) == false)
                {
                    ModelState.AddModelError("File", "Unsupported file extension");
                }

                //// Kiểm tra kích thước tệp
                if (request.LogoUrl.Length > 10485760)// Tệp lớn hơn 10MB
                {
                    ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
                }
            }

        }
        private void ValidateFileUpload(CreateSocialMediaInfoDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.LogoUrl != null)
            {
                //// Kiểm tra phần mở rộng tệp
                if (allowedExtensions.Contains(Path.GetExtension(request.LogoUrl.FileName)) == false)
                {
                    ModelState.AddModelError("File", "Unsupported file extension");
                }

                //// Kiểm tra kích thước tệp
                if (request.LogoUrl.Length > 10485760)// Tệp lớn hơn 10MB
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
