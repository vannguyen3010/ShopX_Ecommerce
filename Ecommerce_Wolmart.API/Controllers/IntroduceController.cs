using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.BannerProduct;
using Shared.DTO.Introduce;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntroduceController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IntroduceController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("CreateIntroduce")]
        public async Task<IActionResult> CreateIntroduce([FromForm] CreateIntroduceDto createIntroduceDto)
        {
            try
            {
                ValidateFileUpload(createIntroduceDto);

                // Kiểm tra xem đối tượng createBannerDto gửi từ client có hợp lệ không
                if (createIntroduceDto == null)
                {
                    _logger.LogError("Introduce object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Introduce object is null",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Introduce object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var introduceEntity = _mapper.Map<Introduce>(createIntroduceDto);

                // Xử lý tập tin hình ảnh
                if (createIntroduceDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createIntroduceDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createIntroduceDto.File.FileName);
                    introduceEntity.FilePath = await SaveFileAndGetUrl(createIntroduceDto.File, fileName, fileExtension);
                    introduceEntity.FileName = fileName;
                    introduceEntity.FileExtension = fileExtension;
                    introduceEntity.FileSizeInBytes = createIntroduceDto.File.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.Introduce.CreateIntroduceAsync(introduceEntity);

                return Ok(new ApiResponse<IntroduceDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<IntroduceDto>(introduceEntity)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Introduce action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllIntroduce")]
        public async Task<IActionResult> GetAllIntroduce()
        {
            try
            {
                var introduces = await _repository.Introduce.GetAllIntroduceAsync(trackChanges: false);
                _logger.LogInfo("Returned all introduce from database.");

                var introducesResult = _mapper.Map<IEnumerable<IntroduceDto>>(introduces);

                return Ok(new ApiResponse<IEnumerable<IntroduceDto>>
                {
                    Success = true,
                    Message = "Banner GetallIntroduce retrieved successfully.",
                    Data = introducesResult
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllIntroduces action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetIntroduceById/{id}")]
        public async Task<IActionResult> GetIntroduceById(Guid id)
        {
            try
            {
                var introduce = await _repository.Introduce.GetIntroduceByIdAsync(id, trackChanges: false);
                if (introduce == null)
                {
                    _logger.LogError($"Không tìm thấy introduce id này {id}");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy introduce id này!",
                        Data = null
                    });
                }

                var introduceResult = _mapper.Map<IntroduceDto>(introduce);
                return Ok(new ApiResponse<IntroduceDto>
                {
                    Success = true,
                    Message = "Banner Products retrieved successfully.",
                    Data = introduceResult
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside introduceId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateIntroduce/{id}")]
        public async Task<IActionResult> UpdateIntroduce(Guid id, [FromForm] UpdateIntroduceDto updateIntroduceDto)
        {
            try
            {
                if (updateIntroduceDto == null)
                {
                    _logger.LogError("Introduce object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy banner id này!",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Introduce object sent from client.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid Introduce object sent from client!",
                        Data = null
                    });
                }

                var introduceEntity = await _repository.Introduce.GetIntroduceByIdAsync(id, trackChanges: true);
                if (introduceEntity == null)
                {
                    _logger.LogError($"Introduce with id: {id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Introduce with id: {id}, hasn't been found in db!",
                        Data = null
                    });
                }

                _mapper.Map(updateIntroduceDto, introduceEntity);

                _repository.Introduce.UpdateIntroduce(introduceEntity);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateIntroduce action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteIntroduce/{id}")]
        public async Task<IActionResult> DeleteIntroduce(Guid id)
        {
            try
            {
                var introduce = await _repository.Introduce.GetIntroduceByIdAsync(id, trackChanges: false);
                if (introduce == null)
                {
                    _logger.LogError($"Introduce with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Introduce.DeleteIntroduce(introduce);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteIntroduce action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        private void ValidateFileUpload(CreateIntroduceDto request)
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
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Introduce", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Introduce/{fileName}{fileExtension}";

            return urlFilePath;
        }
    }
}
