using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.Category;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CateProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CateProductController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("CreateCatogory")]
        public async Task<IActionResult> CreateCateProduct([FromForm] CreateCateProductDto cateProductDto)
        {
            try
            {
                if(cateProductDto == null)
                {
                    _logger.LogError("CategoryProduct object sent from client is null.");
                    return BadRequest("CategoryProduct object is null");
                }
                if(!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryProduct object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var cateProductEntity = _mapper.Map<CateProduct>(cateProductDto);

                // Xử lý tập tin hình ảnh
                if (cateProductDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(cateProductDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(cateProductDto.File.FileName);
                    cateProductEntity.FilePath = await SaveFileAndGetUrl(cateProductDto.File, fileName, fileExtension);
                    cateProductEntity.FileName = fileName;
                    cateProductEntity.FileExtension = fileExtension;
                    cateProductEntity.FileSizeInBytes = cateProductDto.File.Length;
                }

                // Save category to database
                await _repository.CateProduct.CreateCategoryAsync(cateProductEntity);

                return Ok(_mapper.Map<CateProductDto>(cateProductEntity));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void ValidateFileUpload(CreateBannerDto request)
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
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/CateProduct", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/CateProduct/{fileName}{fileExtension}";

            return urlFilePath;
        }
    }
}
