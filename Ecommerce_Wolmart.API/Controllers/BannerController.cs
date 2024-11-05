using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared;
using Repository;
using Microsoft.AspNetCore.Hosting;
using Shared.DTO.Response;
using Shared.DTO.BannerProduct;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BannerController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("CreateBanner")]
        public async Task<IActionResult> CreateBanner([FromForm] CreateBannerDto createbannerDto)
        {
            try
            {
                ValidateFileUpload(createbannerDto);

                // Kiểm tra xem đối tượng createBannerDto gửi từ client có hợp lệ không
                if (createbannerDto == null)
                {
                    _logger.LogError("Banner object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Banner object is null",
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

                // Ánh xạ Dto thành entity
                var bannerEntity = _mapper.Map<Banner>(createbannerDto);

                bannerEntity.Status = true;

                // Xử lý tập tin hình ảnh
                if (createbannerDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createbannerDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createbannerDto.File.FileName);
                    bannerEntity.FilePath = await SaveFileAndGetUrl(createbannerDto.File, fileName, fileExtension);
                    bannerEntity.FileName = fileName;
                    bannerEntity.FileExtension = fileExtension;
                    bannerEntity.FileSizeInBytes = createbannerDto.File.Length;
                }

                // Xử lý tập tin hình ảnh thứ hai
                if (createbannerDto.SecondFile != null)
                {
                    string secondFileName = $"{Guid.NewGuid()}{Path.GetExtension(createbannerDto.SecondFile.FileName)}";
                    var secondFileExtension = Path.GetExtension(createbannerDto.SecondFile.FileName);
                    bannerEntity.SecondFilePath = await SaveFileAndGetUrl(createbannerDto.SecondFile, secondFileName, secondFileExtension);
                    bannerEntity.SecondFileName = secondFileName;
                    bannerEntity.SecondFileExtension = secondFileExtension;
                    bannerEntity.SecondFileSizeInBytes = createbannerDto.SecondFile.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.Banner.CreateBanner(bannerEntity);

                return Ok(new ApiResponse<BannerDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<BannerDto>(bannerEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("CreateBannerTest")]
        public async Task<IActionResult> CreateBannerTest([FromForm] CreateBannerDto createbannerDto)
        {
            try
            {
                ValidateFileUpload(createbannerDto);

                // Kiểm tra object được gửi từ client
                if (createbannerDto == null)
                {
                    _logger.LogError("Banner object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Banner object is null",
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

                // Ánh xạ Dto thành entity
                var bannerEntity = _mapper.Map<Banner>(createbannerDto);

                // Xử lý tập tin hình ảnh
                if (createbannerDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createbannerDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createbannerDto.File.FileName);
                    bannerEntity.FilePath = await SaveFileAndGetRelativePath(createbannerDto.File, fileName, fileExtension);
                    bannerEntity.FileName = fileName;
                    bannerEntity.FileExtension = fileExtension;
                    bannerEntity.FileSizeInBytes = createbannerDto.File.Length;
                }

                // Xử lý tập tin hình ảnh thứ hai
                if (createbannerDto.SecondFile != null)
                {
                    string secondFileName = $"{Guid.NewGuid()}{Path.GetExtension(createbannerDto.SecondFile.FileName)}";
                    var secondFileExtension = Path.GetExtension(createbannerDto.SecondFile.FileName);
                    bannerEntity.SecondFilePath = await SaveFileAndGetRelativePath(createbannerDto.SecondFile, secondFileName, secondFileExtension);
                    bannerEntity.SecondFileName = secondFileName;
                    bannerEntity.SecondFileExtension = secondFileExtension;
                    bannerEntity.SecondFileSizeInBytes = createbannerDto.SecondFile.Length;
                }

                // Lưu vào cơ sở dữ liệu
                await _repository.Banner.CreateBanner(bannerEntity);

                return Ok(new ApiResponse<BannerDto>
                {
                    Success = true,
                    Message = "Banner created successfully.",
                    Data = _mapper.Map<BannerDto>(bannerEntity)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
  
        [HttpGet]
        [Route("GetAllBannerPosition")]
        public async Task<IActionResult> GetAllBannerPosition([FromQuery] BannerPosition? position)
        {
            // Top,0
            // Right,1
            // Left, 2
            // Bottom, 3
            IEnumerable<Banner> banners;
            if(position.HasValue)
            {
                banners = await _repository.Banner.GetAllBannersAsync(position.Value);
            }
            else
            {
                banners = await _repository.Banner.GetAllBannersAsync();
            }
            var bannerDto = _mapper.Map<IEnumerable<BannerDto>>(banners);
            return Ok(new ApiResponse<IEnumerable<BannerDto>>
            {
                Success = true,
                Message = "Banner Products retrieved successfully.",
                Data = bannerDto
            });
        }

        [HttpGet]
        [Route("GetBannerById/{id}")]
        public async Task<IActionResult> GetBannerById(Guid id)
        {
            try
            {
                var banner = await _repository.Banner.GetBannerByIdAsync(id, trackChanges: false);
                if (banner == null)
                {
                    _logger.LogError($"Brand with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned brand with id: {id}");

                return Ok(_mapper.Map<BannerDto>(banner));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetBrandById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateBanner/{id}")]
        public async Task<IActionResult> UpdateBanner(Guid id, [FromForm] BannerUpdateDto updateDto)
        {
            try
            {
                UpdateFileUpload(updateDto);

                if (ModelState.IsValid)
                {
                    var existingBanner = await _repository.Banner.GetBannerByIdAsync(id, trackChanges: false);
                    if (existingBanner == null)
                    {
                        return NotFound("Banner not found");
                    }

                    existingBanner.Title = updateDto.Title;
                    existingBanner.Desc = updateDto.Desc;

                    if (updateDto.File != null)
                    {
                        existingBanner.File = updateDto.File;
                        existingBanner.FileExtension = Path.GetExtension(updateDto.File.FileName);
                        existingBanner.FileSizeInBytes = updateDto.File.Length;
                        existingBanner.FileName = updateDto.File.FileName;
                        //existingBanner.FileDescription = updateDto.FileDescription;
                        existingBanner.FilePath = await SaveFileAndGetUrl(updateDto.File, existingBanner.FileName, existingBanner.FileExtension);
                    }

                    existingBanner.Position = MapBannerPosition(updateDto.Position);

                    await _repository.Banner.UpdateBanner(existingBanner);
                    return Ok(_mapper.Map<BannerDto>(existingBanner));
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateBannerStatus/{id}")]
        public async Task<IActionResult> UpdateBannerStatus(Guid id, [FromQuery] BannerUpdateStatusDto request)
        {
            try
            {
                var existingBanner = await _repository.Banner.GetBannerByIdAsync(id, trackChanges: false);
                if (existingBanner == null)
                {
                    return NotFound("Banner not found");
                }

                existingBanner.Status = request.Status;

                await _repository.Banner.UpdateBanner(existingBanner);

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Banner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteBanner/{id}")]
        public async Task<IActionResult> DeleteBanner(Guid id)
        {
            var banner = await _repository.Banner.GetBannerByIdAsync(id, trackChanges: false);
            if (banner == null)
                return BadRequest("Banner not found");

            // Xóa tệp tin từ hệ thống tệp (nếu cần)
            if(!string.IsNullOrEmpty(banner.FilePath))
            {
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Banner", $"{banner.FileName}{banner.FileExtension}");

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await _repository.Banner.DeleteBanner(id);

            return Ok();
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
        private void UpdateFileUpload(BannerUpdateDto request)
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
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Banner", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Banner/{fileName}{fileExtension}";

            return urlFilePath;
        }

        private async Task<string> SaveFileAndGetRelativePath(IFormFile file, string fileName, string fileExtension)
        {
            var relativeFilePath = Path.Combine("Img_Repository/Banner", $"{fileName}{fileExtension}");
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativeFilePath);

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{relativeFilePath.Replace("\\", "/")}";  // Đảm bảo đường dẫn dùng '/' cho web
        }

        private BannerPosition MapBannerPosition(BannerPosition position)
        {
            return position switch
            {
                BannerPosition.Top => BannerPosition.Top,
                BannerPosition.Right => BannerPosition.Right,
                BannerPosition.Left => BannerPosition.Left,
                BannerPosition.Bottom => BannerPosition.Bottom,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };
        }

    }
}
