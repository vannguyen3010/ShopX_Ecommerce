using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Shared.DTO.Banner;
using Shared.DTO.Category;
using Shared.DTO.Contact;
using Shared.DTO.Product;
using Shared.DTO.Response;

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

        [HttpPost]
        [Route("CreateCategoryProduct")]
        public async Task<IActionResult> CreateCategoryProduct([FromForm] CreateCateProductDto createCategoryDto)
        {
            try
            {
                // Kiểm tra xem đối tượng CategoryProduct gửi từ client có hợp lệ không
                if (createCategoryDto == null)
                {
                    _logger.LogError("CategoryProduct object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"CategoryProduct object is null",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryProduct object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }
                // Kiểm tra nếu tên danh mục đã tồn tại
                var existingCategory = await _repository.CateProduct.GetCategoryByNameAsync(createCategoryDto.Name!);
                if (existingCategory != null)
                {
                    _logger.LogError($"Category with name '{createCategoryDto.Name}' already exists.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Category with name '{createCategoryDto.Name}' already exists.",
                        Data = null
                    });
                }

                // Kiểm tra xem CategoryProduct có phải là cấp 2 không
                if (createCategoryDto.ParentCategoryId.HasValue)
                {
                    // Nếu có parentCategoryId, kiểm tra xem danh mục cấp 1 có tồn tại không
                    var parentCategory = await _repository.CateProduct.GetCategoryProductByIdAsync(createCategoryDto.ParentCategoryId.Value, trackChanges: false);
                    if (parentCategory == null)
                    {
                        _logger.LogError($"Parent category with id {createCategoryDto.ParentCategoryId.Value} not found.");
                        return NotFound(new ApiResponse<Object>
                        {
                            Success = false,
                            Message = $"Parent category with id {createCategoryDto.ParentCategoryId.Value} not found.",
                            Data = null
                        });
                    }
                    // Kiểm tra xem danh mục cấp 1 có sản phẩm không
                    var hasProducts = await _repository.CateProduct.HasProductsInCategoryAsync(createCategoryDto.ParentCategoryId.Value);
                    if(hasProducts)
                    {
                        _logger.LogError("Không thể tạo danh mục con trong danh mục có sản phẩm hiện có.");
                        return NotFound(new ApiResponse<Object>
                        {
                            Success = false,
                            Message = $"Không thể tạo danh mục con trong danh mục có sản phẩm hiện có.",
                            Data = null
                        });
                    }
                }

                // Nếu ParentCategoryId có giá trị, kiểm tra xem danh mục cha có tồn tại hay không
                if (createCategoryDto.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _repository.CateProduct.GetCategoryProductByIdAsync(createCategoryDto.ParentCategoryId.Value, trackChanges: false);
                    if (parentCategory == null)
                    {
                        _logger.LogError($"Parent category with id: {createCategoryDto.ParentCategoryId.Value} not found.");
                        return NotFound(new ApiResponse<Object>
                        {
                            Success = false,
                            Message = $"Parent category with id: {createCategoryDto.ParentCategoryId.Value} not found.",
                            Data = null
                        });
                    }
                }

                // Ánh xạ Dto thành entity
                var cateProductEntity = _mapper.Map<CateProduct>(createCategoryDto);
                // Xử lý tập tin hình ảnh
                if (createCategoryDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createCategoryDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createCategoryDto.File.FileName);
                    cateProductEntity.FilePath = await SaveFileAndGetUrl(createCategoryDto.File, fileName, fileExtension);
                    cateProductEntity.FileName = fileName;
                    cateProductEntity.FileExtension = fileExtension;
                    cateProductEntity.FileSizeInBytes = createCategoryDto.File.Length;
                }

                // Lưu danh mục vào cơ sở dữ liệu
                await _repository.CateProduct.CreateCategoryAsync(cateProductEntity);

                return Ok(new ApiResponse<CateProductDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<CateProductDto>(cateProductEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetCateProductByIdAsync/{id}")]
        public async Task<IActionResult> GetCateProductByIdAsync(Guid id)
        {
            try
            {
                // Lấy category cấp 1 theo id
                var parentCategory = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);
                if (parentCategory == null)
                {
                    _logger.LogError($"Category with id: {id} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Category with id: {id} not found.",
                        Data = null
                    });
                }

                // Lấy danh sách các cấp con của category cấp 1
                var childCategories = await _repository.CateProduct.GetChildCategoriesByParentIdAsync(id, trackChanges: false);


                //Chuyển đổi dữ liệu sang DTO
                var parentCategoryProductDto = _mapper.Map<CateProductDto>(parentCategory);

                //Nếu có các cấp con, thêm chúng vào trong categoriesObj
                if(childCategories != null && childCategories.Any())
                {
                    var childCategoriesDto = _mapper.Map<IEnumerable<CateProductDto>>(childCategories);

                    parentCategoryProductDto.CategoriesObjs = childCategoriesDto.ToList();
                }
                else
                {
                    // Nếu không có cấp con, trả về mảng rỗng
                    parentCategoryProductDto.CategoriesObjs = new List<CateProductDto>();
                }
                return Ok(new ApiResponse<CateProductDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = parentCategoryProductDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetCategoryById action: {ex.Message}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        [HttpGet]
        [Route("GetAllCategoryProducts")]
        public async Task<IActionResult> GetAllCategoryProducts()
        {
            try
            {
                var categories = await _repository.CateProduct.GetAllCategoryProduct(trackChanges: false); // Lấy tất cả danh mục

                var result = categories
                    .Where(c => c.ParentCategoryId == null) // Lấy các danh mục cha
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.FilePath,
                        c.ParentCategoryId,
                        c.CreateAt,
                        c.DateTime,
                        CategoriesObjs = categories
                            .Where(child => child.ParentCategoryId == c.Id) // Lấy danh mục con của danh mục cha hiện tại
                            .Select(child => new
                            {
                                child.Id,
                                child.Name,
                                child.FilePath,
                                child.ParentCategoryId,
                                child.CreateAt,
                                child.DateTime,
                                CategoriesObjs = new List<object>() // Danh mục con của danh mục con (cấp sâu hơn)
                            })
                            .ToList()
                    })
                    .ToList();
                var apiResponse = new ApiResponse<IEnumerable<object>>
                {
                    Success = true,
                    Message = "Categories retrieved successfully.",
                    Data = result
                };
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategories action: {ex.Message}");
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                });
            }
        }

        [HttpDelete]
        [Route("DeleteCategoryProduct")]
        public async Task<IActionResult> DeleteCategoryProduct(Guid id)
        {
            try
            {
                // Kiểm tra xem danh mục có sản phẩm không
                var hasProducts = await _repository.CateProduct.HasProductsInCategoryAsync(id);

                if(hasProducts)
                {
                    _logger.LogError($"Không thể xóa danh mục có id {id} vì nó chứa sản phẩm.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không thể xóa danh mục có id {id} vì nó chứa sản phẩm.",
                        Data = null
                    });
                }

                // Kiểm tra xem danh mục có tồn tại không
                var category = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);

                if (category == null)
                {
                    _logger.LogError($"Không tìm thấy danh mục có id {id}.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy danh mục có id {id}.",
                        Data = null
                    });

                }

                //kiểm tra nếu cấp 1 có cấp con thì không cho xóa
                var childCategories = await _repository.CateProduct.GetChildCategoriesAsync(id);

                if (childCategories.Any())
                {
                    _logger.LogError($"Category with id {id} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không thể xóa danh mục vì nó có các danh mục con. Vui lòng xóa tất cả các danh mục con trước.",
                        Data = null
                    });
                }

                await _repository.CateProduct.DeleteCategoryAsync(category);

                return Ok(new ApiResponse<Object>
                {
                    Success = true,
                    Message = "Category deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateCategoryProduct/{id}")]
        public async Task<IActionResult> UpdateCategoryProduct(Guid id, [FromForm] UpdateCateProductDto updateCateProduct)
        {
            try
            {
                UpdateFileUpload(updateCateProduct);

                if (updateCateProduct == null)
                {
                    _logger.LogError("CategoryProduct object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"CategoryProduct object is null",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryProduct object sent from client.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                var existingCateProduct = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);
                if(existingCateProduct == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Category with id {id} not found.",
                        Data = null
                    });

                }

                // Update category properties
                existingCateProduct.Name = updateCateProduct.Name;

                if (updateCateProduct.File != null)
                {
                    existingCateProduct.File = updateCateProduct.File;
                    existingCateProduct.FilePath = await SaveFileAndGetUrl(updateCateProduct.File, existingCateProduct.FileName, existingCateProduct.FileExtension);
                }

                existingCateProduct.DateTime = DateTime.UtcNow; // Update the DateTime field

                await _repository.CateProduct.UpdateCategoryAsync(existingCateProduct);
                return Ok(new ApiResponse<CateProductDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<CateProductDto>(existingCateProduct)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void UpdateFileUpload(UpdateCateProductDto request)
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
