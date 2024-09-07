using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Introduce;
using Shared.DTO.Product;
using Shared.DTO.Response;

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
                // Kiểm tra nếu tên ntroduce đã tồn tại hay chưa
                var existingProduct = await _repository.Product.GetProductByNameAsync(createProductDto.Name!);
                if (existingProduct != null)
                {
                    _logger.LogError($"Name with name '{existingProduct.Name}' already exists.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Name with name '{existingProduct.Name}' already exists.",
                        Data = null
                    });
                }
                //Kiểm id Danh muc có hợp lệ ko 
                var category = await _repository.CateProduct.GetCategoryProductByIdAsync(createProductDto.CategoryId, trackChanges: false);
                if (category == null)
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
                if (hasChildCategories)
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
        [Route("GetAllProductsPagination")]
        public async Task<IActionResult> GetAllProductsPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Gọi repository để lấy sản phẩm với phân trang
                var (products, totalCount) = await _repository.Product.GetAllProductPaginationAsync(pageNumber, pageSize);

                if (!products.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No products found.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                // Trả về response với data và số lượng sản phẩm
                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = new
                    {
                        totalCount,
                        products = productDtos
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
        [Route("GetAllProductIsHot")]
        public async Task<IActionResult> GetAllProductIsHot()
        {
            try
            {
                // Lấy tất cả sản phẩm có IsHot là true
                var hotProducts = await _repository.Product.GetAllProductIsHotAsync();

                if (hotProducts == null || !hotProducts.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy sản phẩm hot",
                        Data = null
                    });
                }

                // Ánh xạ từ entity sang DTO nếu cần thiết
                var hotProductsDto = _mapper.Map<IEnumerable<ProductDto>>(hotProducts);

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Hot products retrieved successfully.",
                    Data = hotProductsDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProductIsHot action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                //Lấy sản phẩm từ repository
                var product = await _repository.Product.GetProductByIdAsync(id, trackChanges: false);

                if (product == null)
                {
                    _logger.LogError($"Product with id {id} not found.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Product with id {id} not found.",
                        Data = null
                    });
                }

                //Ánh xạ sản phẩm sang DTO để trả về client
                var productDto = _mapper.Map<ProductDto>(product);

                return Ok(new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Product retrieved successfully.",
                    Data = productDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetProductById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetProductsByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Gọi repository để lấy sản phẩm theo CategoryId với phân trang
                var (products, totalCount) = await _repository.Product.GetProductsByCategoryIdAsync(categoryId, pageNumber, pageSize);

                if(!products.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No products found for the given category.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                // Trả về response với dữ liệu phân trang và số lượng sản phẩm
                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = new
                    {
                        totalCount,
                        products = productDtos
                    }
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetProductsByCategoryId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                UpdateFileUpload(updateProductDto);

                //Kiểm tra sản phẩm có tồn tại không
                var product = await _repository.Product.GetProductByIdAsync(id, trackChanges: false);

                if (product == null)
                {
                    _logger.LogError($"Không tìm thấy sản phẩm có id {id}");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy sản phẩm có id {id}",
                        Data = null
                    });
                }

                // Cập nhật các thông tin sản phẩm
                product.Name = updateProductDto.Name;
                product.Description = updateProductDto.Description;
                product.Price = updateProductDto.Price;
                product.IsHot = updateProductDto.IsHot;


                // Nếu có file ảnh mới, cập nhật file if (updateProductDto.ImageFile != null)
                if (updateProductDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(updateProductDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(updateProductDto.File.FileName);
                    product.ImageFilePath = await SaveFileAndGetUrl(updateProductDto.File, fileName, fileExtension);
                }

                // Gọi repository để cập nhật sản phẩm
                 await _repository.Product.UpdateProductAsync(product);

                return Ok(new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<ProductDto>(product)
                });


            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteProductById/{id}")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            try
            {
                //kiểm tra sản phẩm có tồn tại không
                var product = await _repository.Product.GetProductByIdAsync(id, trackChanges: false);
                if (product == null)
                {
                    _logger.LogError($"Product with id {id} not found.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Product with id {id} not found.",
                        Data = null
                    });
                }

                //Xóa Sản phẩm
                await _repository.Product.DeleteProductAsync(product);
                return Ok(new ApiResponse<Object>
                {
                    Success = true,
                    Message = "Product deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside DeleteProductById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("SearchProduct")]
        public async Task<IActionResult> SearchProduct([FromQuery] string keyWord)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(keyWord))
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
                var products = await _repository.Product.SearchProductsByNameAsync(keyWord, trackChanges: false);
                if(products == null || !products.Any())
                {
                    _logger.LogInfo($"Không tìm thấy sản phẩm nào có tên '{keyWord}'.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy sản phẩm nào có tên '{keyWord}'.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Products retrieved successfully.",
                    Data = products.Select(p => _mapper.Map<ProductDto>(p))
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside SearchProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private void UpdateFileUpload(UpdateProductDto request)
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
