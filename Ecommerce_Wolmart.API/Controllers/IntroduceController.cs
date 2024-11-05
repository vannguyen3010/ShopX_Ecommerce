using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.Slug;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.DTO.Introduce;
using Shared.DTO.Product;
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
        private readonly IMemoryCache _cache;

        public IntroduceController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
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

                //Kiểm id Danh muc có hợp lệ ko 
                var category = await _repository.CategoryIntroduce.GetCategoryIntroduceByIdAsync(createIntroduceDto.CategoryId, trackChanges: false);
                if (category == null && category.Status == false)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy id này",
                        Data = null
                    });
                }

                //Kiểm id Danh muc có hợp lệ ko
                if (category.Status == false)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Danh mục này đang trạng thái khóa",
                        Data = null
                    });
                }

                // Kiểm tra nếu tên bài viết đã tồn tại hay chưa
                var existingIntroduce = await _repository.Introduce.GetIntroduceByNameAsync(createIntroduceDto.Name!);
                if (existingIntroduce != null)
                {
                    _logger.LogError($"Title with name '{existingIntroduce.Name}' already exists.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Title with name '{existingIntroduce.Name}' already exists.",
                        Data = null
                    });
                }

                // Ánh xạ Dto thành entity
                var introduceEntity = _mapper.Map<Introduce>(createIntroduceDto);

                // Gán CategoryName cho Introduce entity
                introduceEntity.CategoryName = category.Name;
                introduceEntity.Status = true;
                introduceEntity.FileDescription = createIntroduceDto.Name;
                introduceEntity.FileName = createIntroduceDto.Name;

                // Tạo NameSlug từ Title
                introduceEntity.NameSlug = SlugGenerator.GenerateSlug(createIntroduceDto.Name);

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
        [Route("GetListIntroduce")]
        public async Task<IActionResult> GetListIntroduce([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? categoryId = null, [FromQuery] string? keyword = null, int? type = null)
        {
            try
            {
                // Tạo key cache duy nhất dựa trên tất cả các tham số
                string cacheKey = $"Introduce_{pageNumber}_{pageSize}_{categoryId}_{keyword}_{type}";

                // Kiểm tra xem dữ liệu có tồn tại trong cache không
                if (!_cache.TryGetValue(cacheKey, out (IEnumerable<IntroduceDto> IntroduceDtos, int TotalCount) cachedData))
                {
                    var (introduces, totalCount) = await _repository.Introduce.GetListIntroduceAsync(pageNumber, pageSize, categoryId, keyword, type);

                    if (!introduces.Any())
                    {
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = "Không có bài viết",
                            Data = null
                        });
                    }

                    var introduceDtos = _mapper.Map<IEnumerable<IntroduceDto>>(introduces);

                    cachedData = (introduceDtos, totalCount);

                    // Cấu hình cache với thời gian hết hạn 10 phút
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    // Lưu dữ liệu vào cache
                    _cache.Set(cacheKey, cachedData, cacheOptions);
                }
                else
                {
                    _logger.LogInfo($"Cache hit for Introduces list with key: {cacheKey}");
                }

                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = new
                    {
                        totalCount = cachedData.TotalCount,
                        introduces = cachedData.IntroduceDtos
                    }
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllIntroducesPagination action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllIntroducesPagination")]
        public async Task<IActionResult> GetAllIntroducesPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? keyword = null)
        {
            try
            {
                // Gọi repository để lấy sản phẩm với phân trang
                var (introduces, totalCount) = await _repository.Introduce.GetAllIntroducePaginationAsync(pageNumber, pageSize, keyword);

                if (!introduces.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không có bài viết",
                        Data = null
                    });
                }

                var introduceDtos = _mapper.Map<IEnumerable<IntroduceDto>>(introduces);

                // Trả về response với data và số lượng sản phẩm
                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = new
                    {
                        totalCount,
                        introduces = introduceDtos
                    }
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProducts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllIntroducesByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllIntroducesByCategoryId(Guid categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (introduces, totalCount) = await _repository.Introduce.GetAllProductsByCategoryIdAsync(categoryId, pageNumber, pageSize);

                if (!introduces.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No Introduce found for the given category.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<IntroduceDto>>(introduces);

                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = new
                    {
                        totalCount,
                        introduces = productDtos
                    }
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetProductsByCategoryId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllIntroduceIsHot")]
        public async Task<IActionResult> GetAllIntroduceIsHot()
        {
            try
            {
                // Lấy tất cả sản phẩm có IsHot là true
                var hotIntroduces = await _repository.Introduce.GetAllIntroduceIsHotAsync();

                if (hotIntroduces == null || !hotIntroduces.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy bài viết nổi bật.",
                        Data = null
                    });
                }

                // Ánh xạ từ entity sang DTO nếu cần thiết
                var hotIntroducesDto = _mapper.Map<IEnumerable<IntroduceDto>>(hotIntroduces);

                return Ok(new ApiResponse<IEnumerable<IntroduceDto>>
                {
                    Success = true,
                    Message = "Những bài viết nổi bật.",
                    Data = hotIntroducesDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProductIsHot action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetIntroduceById/{id}")]
        public async Task<IActionResult> GetIntroduceById(Guid id)
        {
            try
            {
                // Tạo key cache duy nhất cho từng sản phẩm theo id
                string cacheKey = $"Introduce_{id}";

                if (!_cache.TryGetValue(cacheKey, out ApiProductResponse<IntroduceDto, IEnumerable<IntroduceDto>> cachedResponse))
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

                    // Lấy danh sách bài viết liên quan (ví dụ: cùng danh mục)
                    var relateIntroduces = await _repository.Introduce.GetRelatedIntroducesAsync(id, introduce.CategoryId, trackChanges: true);

                    var introduceDto = _mapper.Map<IntroduceDto>(introduce);

                    var introduceResult = _mapper.Map<IEnumerable<IntroduceDto>>(relateIntroduces);

                    cachedResponse =new ApiProductResponse<IntroduceDto, IEnumerable<IntroduceDto>>
                    {
                        Success = true,
                        Message = "Introduce retrieved successfully.",
                        Data = introduceDto,
                        Data2nd = introduceResult // Trả về bài viết liên quan trong Data2nd
                    };

                    var cacheOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromMinutes(10)) // Gia hạn nếu được truy cập
                     .SetAbsoluteExpiration(TimeSpan.FromHours(1));  // Cache hết hạn sau 1 giờ

                    _cache.Set(cacheKey, cachedResponse, cacheOptions);
                }
                else
                {
                    _logger.LogInfo($"Cache hit for introduce with id: {id}");
                }

                return Ok(cachedResponse);


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

                var category = await _repository.CategoryIntroduce.GetCategoryIntroduceByIdAsync(updateIntroduceDto.CategoryId, trackChanges: false);
                if (category == null)
                {
                    _logger.LogError($"Introduce category with id: {id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Introduce category with id: {id}, hasn't been found in db!",
                        Data = null
                    });
                }

                _mapper.Map(updateIntroduceDto, introduceEntity);

                introduceEntity.NameSlug = SlugGenerator.GenerateSlug(updateIntroduceDto.Name);
                introduceEntity.UpdatedAt = DateTime.UtcNow;
                introduceEntity.CategoryName = category.Name;
                introduceEntity.CategoryId = category.Id;

                if (updateIntroduceDto.File != null)
                {
                    string mainImageFileName = $"{Guid.NewGuid()}{Path.GetExtension(updateIntroduceDto.File.FileName)}";
                    introduceEntity.FilePath = await SaveFileAndGetUrl(updateIntroduceDto.File, mainImageFileName, Path.GetExtension(updateIntroduceDto.File.FileName));
                    introduceEntity.FileName = mainImageFileName;
                    introduceEntity.FileExtension = Path.GetExtension(updateIntroduceDto.File.FileName);
                    introduceEntity.FileSizeInBytes = updateIntroduceDto.File.Length;
                }

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

        [HttpPut]
        [Route("UpdateIntroduceStatus/{id}")]
        public async Task<IActionResult> UpdateIntroduceStatus(Guid id, [FromQuery] UpdateIntroduceStatusDto updateIntroduceDto)
        {
            try
            {

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

                introduceEntity.Status = updateIntroduceDto.Status;

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

        [HttpGet]
        [Route("SearchIntroduce/{keyWord}")]
        public async Task<IActionResult> SearchIntroduce([FromQuery] string keyWord)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyWord))
                {
                    _logger.LogError("Tên tìm kiếm không được để trống.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Tên tìm kiếm không được để trống..",
                        Data = null
                    });
                }

                // Tìm kiếm sản phẩm theo tên với chuỗi con
                var introduces = await _repository.Introduce.SearchIntroducesByNameAsync(keyWord, trackChanges: false);
                if (introduces == null || !introduces.Any())
                {
                    _logger.LogInfo($"Không tìm thấy bài viết nào có tên '{keyWord}'.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy bài viết nào có tên '{keyWord}'.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<IntroduceDto>>
                {
                    Success = true,
                    Message = "Products retrieved successfully.",
                    Data = introduces.Select(p => _mapper.Map<IntroduceDto>(p))
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside SearchProduct action: {ex.Message}");
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
        //private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        //{
        //    var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Introduce", $"{fileName}{fileExtension}");

        //    using var stream = new FileStream(localFilePath, FileMode.Create);
        //    await file.CopyToAsync(stream);

        //    var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Introduce/{fileName}{fileExtension}";

        //    return urlFilePath;
        //}
        private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        {
            var relativeFilePath = Path.Combine("Img_Repository/Introduce", $"{fileName}{fileExtension}");
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativeFilePath);

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{relativeFilePath.Replace("\\", "/")}";  // Đảm bảo đường dẫn dùng '/' cho web
        }
    }
}
