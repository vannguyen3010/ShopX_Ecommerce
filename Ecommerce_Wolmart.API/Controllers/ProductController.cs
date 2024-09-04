using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.Product;
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
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
        {
            ValidateFileUpload(productDto);
            try
            {
                if (productDto == null)
                {
                    _logger.LogError("Product object sent from client is null.");
                    return BadRequest("Product object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Product object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var productEntity = _mapper.Map<Product>(productDto);

                //Xử lý hình ảnh
                if (productDto.ImageFile != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDto.ImageFile.FileName)}";
                    var fileExtension = Path.GetExtension(productDto.ImageFile.FileName);
                    productEntity.ImageFilePath = await SaveFileAndGetUrl(productDto.ImageFile, fileName, fileExtension);
                    productEntity.ImageFileName = fileName;
                    productEntity.ImageFileExtension = fileExtension;
                    productEntity.ImageFileSizeInBytes = productDto.ImageFile.Length;
                }

                //Save product to db
                await _repository.Product.CreateProductAsync(productEntity);

                return Ok(_mapper.Map<ProductDto>(productEntity));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateBrand action: {ex.Message}");
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
