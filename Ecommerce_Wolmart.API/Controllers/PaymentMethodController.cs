using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.Migrations;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Payment;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentMethodController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("CreatePaymentMethod")]
        public async Task<IActionResult> CreatePaymentMethod([FromForm] CreatePaymentDto createpaymentDto)
        {
            try
            {
                ValidateFileUpload(createpaymentDto);

                if (createpaymentDto == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "PaymentMethodDto is null.",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid PaymentMethodDto object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }


                // Ánh xạ Dto thành entity
                var paymentEntity = _mapper.Map<PaymentMethod>(createpaymentDto);

                // Xử lý tập tin hình ảnh
                if (createpaymentDto.File != null)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createpaymentDto.File.FileName)}";
                    var fileExtension = Path.GetExtension(createpaymentDto.File.FileName);
                    paymentEntity.FilePath = await SaveFileAndGetUrl(createpaymentDto.File, fileName, fileExtension);
                    paymentEntity.FileName = fileName;
                    paymentEntity.FileExtension = fileExtension;
                    paymentEntity.FileSizeInBytes = createpaymentDto.File.Length;
                }

                // tạo danh mục vào cơ sở dữ liệu
                await _repository.PaymentMethod.CreatePaymentMethod(paymentEntity);

                var result = await _repository.PaymentMethod.SaveAsync();
                if (!result)
                {
                    _logger.LogError("Error creating payment method.");
                    return StatusCode(500, "Internal server error");
                }

                return Ok(new ApiResponse<PaymentMethodDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<PaymentMethodDto>(paymentEntity)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating payment method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet]
        [Route("GetAllPaymentMethod")]
        public async Task<IActionResult> GetAllPaymentMethod()
        {
            try
            {
                var paymentsMethods = await _repository.PaymentMethod.GetAllPaymentMethodsAsync();
                if (paymentsMethods == null)
                {
                    _logger.LogError("Không có phương thức thánh toán nào!");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không có phương thức thánh toán nào!",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<PaymentMethodDto>>
                {
                    Success = true,
                    Message = "Phương thức thanh toán được truy xuất thành công!",
                    Data = _mapper.Map<IEnumerable<PaymentMethodDto>>(paymentsMethods)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error retrieving payment methods: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetPaymentMethodById/{Id}")]
        public async Task<IActionResult> GetPaymentMethodById(Guid Id)
        {
            try
            {
                var paymentMethod = await _repository.PaymentMethod.GetPaymentMethodByIdAsync(Id, trackChanges: false);
                if (paymentMethod == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Payment method not found.",
                        Data = null
                    });
                }

                //return Ok(new { Success = true, Message = "Payment method retrieved successfully.", Data = paymentMethod });
                return Ok(new ApiResponse<PaymentMethodDto>
                {
                    Success = true,
                    Message = "Payment method retrieved successfully.",
                    Data = _mapper.Map<PaymentMethodDto>(paymentMethod)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving payment method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdatePaymentMethod/{Id}")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid Id, [FromForm] UpdatePaymentDto updatePaymentDto)
        {
            try
            {
                if (updatePaymentDto == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Payment method not found.",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Payment method object sent from client.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid Payment method object sent from client!",
                        Data = null
                    });
                }

                var existingPaymentMethod = await _repository.PaymentMethod.GetPaymentMethodByIdAsync(Id, trackChanges: false);
                if (existingPaymentMethod == null)
                {
                    _logger.LogError($"Payment method with id: {Id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Payment method with id: {id}, hasn't been found in db!",
                        Data = null
                    });
                }
                // Map DTO to entity
                _mapper.Map(updatePaymentDto, existingPaymentMethod);

                if (updatePaymentDto.File != null)
                {
                    existingPaymentMethod.File = updatePaymentDto.File;
                    existingPaymentMethod.FilePath = await SaveFileAndGetUrl(updatePaymentDto.File, existingPaymentMethod.FileName, existingPaymentMethod.FileExtension);
                }

                existingPaymentMethod.UpdatedAt = DateTime.UtcNow; // Update the DateTime field

                _repository.PaymentMethod.UpdatePaymentMethod(existingPaymentMethod);
                _repository.SaveAsync();

                return NoContent();

            }
            catch (Exception ex)
            {

                _logger.LogError("Error updating payment method.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeletePaymentMethod/{Id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid Id)
        {
            try
            {
                var paymentMethod = await _repository.PaymentMethod.GetPaymentMethodByIdAsync(Id, trackChanges: false);
                if (paymentMethod == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Payment method not found.",
                        Data = null
                    });
                }

                _repository.PaymentMethod.DeletePaymentMethod(paymentMethod);
                var result = await _repository.PaymentMethod.SaveAsync();

                if (!result)
                {
                    _logger.LogError("Error deleting payment method.");
                    return StatusCode(500, "Internal server error");
                }
                return Ok(new { Success = true, Message = "Payment method deleted successfully." });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> SaveFileAndGetUrl(IFormFile file, string fileName, string fileExtension)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Payment", $"{fileName}{fileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Payment/{fileName}{fileExtension}";

            return urlFilePath;
        }
        private void ValidateFileUpload(CreatePaymentDto request)
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
