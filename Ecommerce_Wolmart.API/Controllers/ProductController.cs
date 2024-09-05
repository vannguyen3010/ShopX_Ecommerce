using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.Category;
using Shared.DTO.Product;
using Shared.DTO.Response;
using System.Net.Http;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            ValidateFileUpload(createProductDto);
            try
            {
                if (createProductDto == null)
                {
                    _logger.LogError("Product object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Product object is null",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Product object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid Product object sent from client.",
                        Data = null
                    });
                }

                //Kiểm id Danh muc có hợp lệ ko 
                var category = await _repository.CateProduct.GetCategoryProductByIdAsync(createProductDto.CategoryId, trackChanges: false);
                if(category == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid category ID.",
                        Data = null
                    });
                }

                //Kiểm tra danh mục 1 có danh mục con không
                var hasChildCategories = await _repository.CateProduct.HasChildCategoriesAsync(createProductDto.CategoryId);
                if(hasChildCategories)
                {
                    _logger.LogError("Không thể tạo danh mục con trong danh mục có sản phẩm hiện có.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không thể tạo danh mục con trong danh mục có sản phẩm hiện có.",
                        Data = null
                    });
                }

                var productEntity = _mapper.Map<Product>(createProductDto);

                //Xử lý hình ảnh
                if (createProductDto.ImageFile != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createProductDto.ImageFile.FileName)}";
                    var fileExtension = Path.GetExtension(createProductDto.ImageFile.FileName);
                    productEntity.ImageFilePath = await SaveFileAndGetUrl(createProductDto.ImageFile, fileName, fileExtension);
                    productEntity.ImageFileName = fileName;
                    productEntity.ImageFileExtension = fileExtension;
                    productEntity.ImageFileSizeInBytes = createProductDto.ImageFile.Length;
                }

                //Save product to db
                await _repository.Product.CreateProductAsync(productEntity);

                return Ok(new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Category created successfully.",
                    Data = _mapper.Map<ProductDto>(productEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateBrand action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _repository.Product.GetAllProductAsync();
                if(products == null)
                {
                    _logger.LogInfo("No products found.");
                    return NotFound(new ApiResponse<IEnumerable<ProductDto>>
                    {
                        Success = false,
                        Message = "No products found.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Products retrieved successfully.",
                    Data = productDtos
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProducts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Product", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Product/{fileName}{fileExtension}";

            return urlFilePath;
        }
        private void ValidateFileUpload(CreateProductDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.ImageFile != null)
            {
                //// Kiểm tra phần mở rộng tệp
                if (allowedExtensions.Contains(Path.GetExtension(request.ImageFile.FileName)) == false)
                {
                    ModelState.AddModelError("File", "Unsupported file extension");
                }

                //// Kiểm tra kích thước tệp
                if (request.ImageFile.Length > 10485760)// Tệp lớn hơn 10MB
                {
                    ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
                }
            }

        }
    }
}
