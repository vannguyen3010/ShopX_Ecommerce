﻿using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.Slug;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Repository;
using Shared.DTO.CateProduct;
using Shared.DTO.Introduce;
using Shared.DTO.Product;
using Shared.DTO.Response;
using System.Collections;
using System.Collections.Generic;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RepositoryContext _dbContext;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMemoryCache _cache;

        public ProductController(ILoggerManager logger, IRepositoryManager repository, RepositoryContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, IMemoryCache cache)
        {
            _logger = logger;
            _repository = repository;
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _cache = cache;
        }

        [HttpPost]
        [Route("CreateProduct")]
        [Authorize]
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
                // Kiểm tra nếu tên sản phẩm đã tồn tại hay chưa
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

                ////Kiểm tra danh mục 1 có danh mục con không
                //var hasChildCategories = await _repository.CateProduct.HasChildCategoriesAsync(createProductDto.CategoryId);
                //if (hasChildCategories)
                //{
                //    _logger.LogError("Không thể tạo danh mục chứa các danh mục con của danh mục này.");
                //    return NotFound(new ApiResponse<Object>
                //    {
                //        Success = false,
                //        Message = $"Không thể tạo danh mục chứa các danh mục con của danh mục này.",
                //        Data = null
                //    });
                //}

                // Kiểm tra Discount lớn hơn Price hay không
                if (createProductDto.Discount > createProductDto.Price)
                {
                    _logger.LogError("Sản phẩm giảm giá không được lớn hơn giá gốc.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Sản phẩm giảm giá không được lớn hơn giá gốc.",
                        Data = null
                    });
                }


                var productEntity = _mapper.Map<Product>(createProductDto);

                // Lấy CategoryName từ danh mục
                productEntity.CategoryName = category.Name;

                // Tạo NameSlug từ Title
                productEntity.NameSlug = SlugGenerator.GenerateSlug(createProductDto.Name);

                productEntity.Status = true;

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

                // Xử lý các hình ảnh con
                if (createProductDto.ImageObjectList != null)
                {
                    foreach (var file in createProductDto.ImageObjectList)
                    {
                        var productImage = new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productEntity.Id,
                            ImageFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}",
                            ImageFilePath = await SaveFileAndGetUrl(file, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}", Path.GetExtension(file.FileName)),
                            ImageFileExtension = Path.GetExtension(file.FileName),
                            ImageFileSizeInBytes = file.Length
                        };
                        productEntity.ProductImages.Add(productImage);
                    }
                }


                //Save product to db
                await _repository.Product.CreateProductAsync(productEntity);

                return CreatedAtAction(nameof(CreateProduct), new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Product created successfully.",
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
        [Route("GetListProduct")]
        public async Task<IActionResult> GetListProduct([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            try
            {
                var (products, totalCount) = await _repository.Product.GetListProducAsync(pageNumber, pageSize, minPrice, maxPrice, categoryId, keyword, type);

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

                _logger.LogError($"Something went wrong inside GetAllProductsPagination action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllProductsPagination")]
        public async Task<IActionResult> GetAllProductsPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
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
        [Route("GetAllProductDiscount")]
        public async Task<IActionResult> GetAllProductDiscount()
        {
            try
            {
                var discountedProducts = await _repository.Product.GetAllProductDiscountAsync(trackChanges: false);
                if (!discountedProducts.Any())
                {
                    _logger.LogInfo("No discounted products found.");
                    return NotFound(new ApiResponse<IEnumerable<ProductDto>>
                    {
                        Success = false,
                        Message = "No discounted products found",
                        Data = null
                    });
                }
                // Ánh xạ từ entity sang DTO nếu cần thiết
                var discountProductDto = _mapper.Map<IEnumerable<ProductDto>>(discountedProducts);
                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Discount products retrieved successfully.",
                    Data = discountProductDto
                });

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProductIsHot action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Lấy tất cả sản phẩm hết hàng
        [HttpGet]
        [Route("GetAllProductOutByStockStatus")]
        public async Task<IActionResult> GetAllProductOutByStockStatus()
        {
            try
            {
                // Truy vấn tất cả sản phẩm có StockQuantity bằng 0
                var products = await _repository.Product.GetAllProductOutByStockStatusAsync(0, trackChanges: false);

                if (products == null || !products.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Không có sản phẩm nào hết hàng.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Danh sách sản phẩm hết hàng.",
                    Data = productDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy danh sách sản phẩm hết hàng: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Lấy tất cả sản phẩm còn hàng
        [HttpGet]
        [Route("GetAllProductOnByStockStatus")]
        public async Task<IActionResult> GetAllProductOnByStockStatus()
        {
            try
            {
                //Lấy tất cả sản phẩm từ cơ sở dữ liệu, với điều kiện StockQuantity > 0
                var products = await _repository.Product.GetAllProductOnByStockStatusAsync(stockQuantity: 0, trackChanges: false);

                if (products == null || !products.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Không có sản phẩm nào còn hàng.",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Danh sách sản phẩm hết hàng.",
                    Data = productDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy danh sách sản phẩm hết hàng: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Lấy tất cả sản phẩm mới
        [HttpGet]
        [Route("GetAllNewProducts")]
        public async Task<IActionResult> GetAllNewProducts(DateTime? startDate = null) //2-10
        {
            try
            {
                // Nếu không cung cấp startDate, đặt ngày mặc định là 10 ngày trước ngày hiện tại
                startDate ??= DateTime.UtcNow.AddDays(-10);

                // Lấy tất cả sản phẩm mới từ cơ sở dữ liệu, với điều kiện ngày tạo lớn hơn startDate
                var products = await _repository.Product.GetAllNewProductsAsync(startDate.Value, trackChanges: false);

                if (products == null || !products.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Không có sản phẩm mới",
                        Data = null
                    });
                }

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Danh sách sản phẩm mới.",
                    Data = productDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy danh sách sản phẩm mới: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllBestSellingProducts")]
        public async Task<IActionResult> GetAllBestSellingProducts(int bestSeller)
        {
            try
            {
                var bestSellingProducts = await _repository.Product.GetBestSellingProductsAsync(bestSeller, trackChanges: false);
                if (bestSellingProducts == null || !bestSellingProducts.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy sản phẩm bán chạy.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Best-selling products retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<ProductDto>>(bestSellingProducts)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetBestSellingProducts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _repository.Product.GetProductByIdAsync(id, trackChanges: false);

                if (product == null)
                {
                    _logger.LogError($"Product with id {id} not found.");
                    return NotFound(new ApiProductResponse<Object, Object>
                    {
                        Success = false,
                        Message = $"Product with id {id} not found.",
                        Data = null,
                        Data2nd = null
                    });
                }

                var category = await _repository.CateProduct.GetCategoryProductByIdAsync(product.CategoryId, trackChanges: false);

                if (category == null)
                {
                    _logger.LogError($"Category for product with id {id} not found.");
                    return NotFound(new ApiProductResponse<Object, Object>
                    {
                        Success = false,
                        Message = $"Category for product with id {id} not found.",
                        Data = null,
                        Data2nd = null
                    });
                }

                //Kiểm tra danh mục có cấp cha không nếu có
                CateProductDto parentCategoryDto = null;
                if (category.ParentCategoryId != null)
                {
                    var parentCategory = await _repository.CateProduct.GetCategoryProductByIdAsync((Guid)category.ParentCategoryId, trackChanges: false);
                    if (parentCategory != null)
                    {
                        parentCategoryDto = _mapper.Map<CateProductDto>(parentCategory);
                    }
                }

                //Ánh xạ sản phẩm sang DTO để trả về client
                var productDto = _mapper.Map<ProductDto>(product);

                // Ánh xạ danh mục cha vào DTO sản phẩm
                productDto.ParentCategory = parentCategoryDto;

                // Lấy danh sách sản phẩm liên quan (ví dụ: cùng danh mục)
                var relatedProducts = await _repository.Product.GetRelatedProductsAsync(id, product.CategoryId, trackChanges: true);

                var relatedProductsDto = _mapper.Map<IEnumerable<ProductDto>>(relatedProducts);

                // Tạo phản hồi và lưu vào cache
                return Ok (new ApiProductResponse<ProductDto, IEnumerable<ProductDto>>
                {
                    Success = true,
                    Message = "Product retrieved successfully.",
                    Data = productDto,
                    Data2nd = relatedProductsDto
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
                // Tạo key duy nhất cho cache theo categoryId, pageNumber và pageSize
                string cacheKey = $"Products_Category_{categoryId}_Page_{pageNumber}_Size_{pageSize}";

                // Kiểm tra xem dữ liệu có tồn tại trong cache không
                if (!_cache.TryGetValue(cacheKey, out ProductResponseDto cachedProductResponse))
                {
                    var (products, totalCount) = await _repository.Product.GetProductsByCategoryIdAsync(categoryId, pageNumber, pageSize);

                    if (!products.Any())
                    {
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = "No products found for the given category.",
                            Data = null
                        });
                    }

                    var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);


                    cachedProductResponse = new ProductResponseDto
                    {
                        TotalCount = totalCount,
                        Products = productDtos
                    };

                    // Cấu hình cache để hết hạn sau 10 phút
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    // Lưu dữ liệu vào cache
                    _cache.Set(cacheKey, cachedProductResponse, cacheOptions);
                }
                else
                {
                    _logger.LogInfo($"Cache hit for products in category: {categoryId}, page: {pageNumber}, size: {pageSize}");
                }

                return Ok(new
                {
                    success = true,
                    message = "Products retrieved successfully.",
                    data = cachedProductResponse
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetProductsByCategoryId action: {ex.Message}");
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

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                UpdateFileUpload(updateProductDto);

                //Kiểm tra sản phẩm có tồn tại không
                var productEntity = await _repository.Product.GetProductByIdAsync(id, trackChanges: true);

                if (productEntity == null)
                {
                    _logger.LogError($"Không tìm thấy sản phẩm có id {id}");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy sản phẩm có id {id}",
                        Data = null
                    });
                }

                // Kiểm tra Discount lớn hơn Price không
                if (updateProductDto.Discount > updateProductDto.Price)
                {
                    _logger.LogError("Discount cannot be greater than price.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Discount cannot be greater than price.",
                        Data = null
                    });
                }

                // Kiểm tra nếu tên sản phẩm đã tồn tại hay chưa
                var existingProduct = await _repository.Product.GetProductByNameAsync(updateProductDto.Name!);
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

                // Chuyển đổi RowVersion từ chuỗi hexadecimal sang byte[]
                //byte[] rowVersionBytes = ConvertHexStringToByteArray(updateProductDto.RowVersion);

                //Cập nhật các thông tin sản phẩm
                _mapper.Map(updateProductDto, productEntity);
                productEntity.NameSlug = SlugGenerator.GenerateSlug(updateProductDto.Name);
                productEntity.UpdatedAt = DateTime.UtcNow;

                // Nếu có file ảnh mới, cập nhật file if (updateProductDto.ImageFile != null)
                if (updateProductDto.File != null)
                {

                    string mainImageFileName = $"{Guid.NewGuid()}{Path.GetExtension(updateProductDto.File.FileName)}";
                    productEntity.ImageFilePath = await SaveFileAndGetUrl(updateProductDto.File, mainImageFileName, Path.GetExtension(updateProductDto.File.FileName));
                    productEntity.ImageFileName = mainImageFileName;
                    productEntity.ImageFileExtension = Path.GetExtension(updateProductDto.File.FileName);
                    productEntity.ImageFileSizeInBytes = updateProductDto.File.Length;
                }

                // Xử lý hình ảnh con nếu có
                if (updateProductDto.ImageObjectList != null && updateProductDto.ImageObjectList.Count > 0)
                {
                    // Remove existing images
                    productEntity.ProductImages.Clear();

                    foreach (var file in updateProductDto.ImageObjectList)
                    {
                        var productImage = new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ProductId = productEntity.Id,
                            ImageFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}",
                            ImageFilePath = await SaveFileAndGetUrl(file, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}", Path.GetExtension(file.FileName)),
                            ImageFileExtension = Path.GetExtension(file.FileName),
                            ImageFileSizeInBytes = file.Length
                        };
                        productEntity.ProductImages.Add(productImage);
                    }
                }

                // Cập nhật sản phẩm trong DB
                //await _repository.Product.UpdateProductAsync(productEntity, rowVersionBytes);
                await _repository.Product.UpdateProductAsync(productEntity);

                return Ok(new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Sản phẩm được cập nhật thành công.",
                    Data = _mapper.Map<ProductDto>(productEntity)
                });

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency conflict occurred: {ex.Message}");
                return Conflict(new ApiResponse<Object>
                {
                    Success = false,
                    Message = "Concurrency conflict occurred. Please try again.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateProductStatus/{id}")]
        public async Task<IActionResult> UpdateProductStatus(Guid id, [FromQuery] UpdateProductStatusDto request)
        {
            try
            {
                var productEntity = await _repository.Product.GetProductByIdAsync(id, trackChanges: true);

                if (productEntity == null)
                {
                    _logger.LogError($"Không tìm thấy sản phẩm có id {id}");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy sản phẩm có id {id}",
                        Data = null
                    });
                }

                _mapper.Map(request, productEntity);
                
                productEntity.Status = request.Status;

                await _repository.Product.UpdateProductAsync(productEntity);

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        private byte[] ConvertHexStringToByteArray(string hex)
        {
            if (hex.StartsWith("0x"))
                hex = hex.Substring(2);

            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }
        private void UpdateFileUpload(UpdateProductDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (request.File != null)
            {
                ////// Kiểm tra phần mở rộng tệp
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
