using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTO.BannerProduct;
using Shared.DTO.Product;
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
        public async Task<IActionResult> CreateBannerProduct([FromForm] CreateBannerProductDto createBannerDto)
        {
            try
            {
                ValidateFileUpload(createBannerDto);
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

        [HttpGet]
        [Route("GetAllBannerPositionProductPopup")]
        public async Task<IActionResult> GetAllBannerPositionProductPopup([FromQuery] BannerProductWithPopupPosition? position)
        {
            try
            {
                IEnumerable<BannerProduct> banners;
                if (position.HasValue)
                {
                    banners = await _repository.BannerProduct.GetAllBannerProductAsync(position);
                }
                else
                {
                    banners = await _repository.BannerProduct.GetAllBannerProductAsync();
                }

                var bannerDto = _mapper.Map<IEnumerable<BannerProductDto>>(banners);

                return Ok(new ApiResponse<IEnumerable<BannerProductDto>>
                {
                    Success = true,
                    Message = "Banner Products retrieved successfully.",
                    Data = bannerDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside BannerProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet]
        [Route("GetBannerProductById/{id}")]
        public async Task<IActionResult> GetBannerProductById(Guid id)
        {
            try
            {
                var banner = await _repository.BannerProduct.GetBannerProductbyId(id, trackChanges: false);
                if (banner == null)
                {
                    _logger.LogError($"Không tìm thấy banner id này {id}");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy banner id này!",
                        Data = null
                    });
                }

                var bannerResult = _mapper.Map<BannerProductDto>(banner);
                return Ok(new ApiResponse<BannerProductDto>
                {
                    Success = true,
                    Message = "Banner Products retrieved successfully.",
                    Data = bannerResult
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetBrandById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateBannerProduct/{id}")]
        public async Task<IActionResult> UpdateBannerProduct(Guid id, [FromForm] UpdateBannerProductDto updateBannerDto)
        {
            try
            {
                if (updateBannerDto == null)
                {
                    _logger.LogError("Banner object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy banner id này!",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Banner object sent from client.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid Banner object sent from client!",
                        Data = null
                    });
                }

                var bannerEntity = await _repository.BannerProduct.GetBannerProductbyId(id, trackChanges: true);
                if (bannerEntity == null)
                {
                    _logger.LogError($"Banner with id: {id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Banner with id: {id}, hasn't been found in db!",
                        Data = null
                    });
                }

                _mapper.Map(updateBannerDto, bannerEntity);

                _repository.BannerProduct.UpdateBannerProduct(bannerEntity);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateBrand action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteBannerProduct/{id}")]
        public async Task<IActionResult> DeleteBannerProduct(Guid id)
        {
            try
            {
                var banner = await _repository.BannerProduct.GetBannerProductbyId(id, trackChanges: false);
                if (banner == null)
                {
                    _logger.LogError($"Brand with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.BannerProduct.DeleteBannerProduct(banner);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteBrand action: {ex.Message}");
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

        private void ValidateFileUpload(CreateBannerProductDto request)
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
    }
}
