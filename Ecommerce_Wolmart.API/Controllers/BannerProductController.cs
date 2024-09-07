using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.BannerProduct;
using Shared.DTO.Category;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannerProductController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("CreateBannerProduct")]
        public async Task<IActionResult> CreateBannerProduct([FromForm] CreateBannerProduct createBannerDto)
        {
            try
            {
                // Kiểm tra xem đối tượng createBannerDto gửi từ client có hợp lệ không
                if (createBannerDto == null)
                {
                    _logger.LogError("BannerProduct object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"BannerProduct object is null",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid BannerProduct object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var bannerProductEntity = _mapper.Map<BannerProduct>(createBannerDto);

                // Xử lý tập tin hình ảnh
                if (createBannerDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createBannerDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createBannerDto.File.FileName);
                    bannerProductEntity.FilePath = await SaveFileAndGetUrl(createBannerDto.File, fileName, fileExtension);
                    bannerProductEntity.FileName = fileName;
                    bannerProductEntity.FileExtension = fileExtension;
                    bannerProductEntity.FileSizeInBytes = createBannerDto.File.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.BannerProduct.CreateBannerProductAsync(bannerProductEntity);

                return Ok(new ApiResponse<BannerProductDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<BannerProductDto>(bannerProductEntity)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside BannerProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/BannerProduct", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/BannerProduct/{fileName}{fileExtension}";

            return urlFilePath;
        }
    }
}
