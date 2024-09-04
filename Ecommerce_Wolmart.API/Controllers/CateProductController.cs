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
        public async Task<IActionResult> CreateCategoryProduct([FromForm] CreateCateProductDto cateProductDto)
        {
            try
            {
                if (cateProductDto == null)
                {
                    _logger.LogError("CategoryProduct object sent from client is null.");
                    return BadRequest("CategoryProduct object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryProduct object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Kiểm tra nếu tên danh mục đã tồn tại
                var existingCategory = await _repository.CateProduct.GetCategoryByNameAsync(cateProductDto.Name!);
                if (existingCategory != null)
                {
                    _logger.LogError($"Category with name '{cateProductDto.Name}' already exists.");
                    return BadRequest($"Category with name '{cateProductDto.Name}' already exists.");
                }

                // Nếu ParentCategoryId có giá trị, kiểm tra xem danh mục cha có tồn tại hay không
                if (cateProductDto.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _repository.CateProduct.GetCategoryProductByIdAsync(cateProductDto.ParentCategoryId.Value, trackChanges: false);
                    if (parentCategory == null)
                    {
                        _logger.LogError($"Parent category with id: {cateProductDto.ParentCategoryId.Value} not found.");
                        return BadRequest($"Parent category with id: {cateProductDto.ParentCategoryId.Value} not found.");
                    }
                }

                // Ánh xạ Dto thành entity
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

                // Lưu danh mục vào cơ sở dữ liệu
                await _repository.CateProduct.CreateCategoryAsync(cateProductEntity);

                return Ok(_mapper.Map<CateProductDto>(cateProductEntity));
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
                var cateProduct = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);
                if (cateProduct == null)
                {
                    _logger.LogError($"Category with id: {id} not found.");
                    return NotFound($"Category with id: {id} not found.");
                }

                return Ok(_mapper.Map<CateProductDto>(cateProduct));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetCategoryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
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

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategories action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteCategoryProduct")]
        public async Task<IActionResult> DeleteCategoryProduct(Guid id)
        {
            try
            {
                var categoryProduct = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);

                if (categoryProduct == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    return NotFound($"Category with id {id} not found.");
                }

                //kiểm tra nếu cấp 1 có cấp con thì không cho xóa
                var childCategories = await _repository.CateProduct.GetChildCategoriesAsync(id);

                if (childCategories.Any())
                {
                    return BadRequest("Không thể xóa danh mục vì nó có các danh mục con. Vui lòng xóa tất cả các danh mục con trước.");
                }

                await _repository.CateProduct.DeleteCategoryAsync(categoryProduct);

                return Ok();
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
            UpdateFileUpload(updateCateProduct);
            try
            {
                if (updateCateProduct == null)
                {
                    _logger.LogError("CategoryProduct object sent from client is null.");
                    return BadRequest("CategoryProduct object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryProduct object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var existingCateProduct = await _repository.CateProduct.GetCategoryProductByIdAsync(id, trackChanges: false);
                if(existingCateProduct == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    return NotFound($"Category with id {id} not found.");
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
                return Ok(_mapper.Map<CateProductDto>(existingCateProduct));
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
