using AutoMapper;
using Contracts;
using EmailService;
using Entities.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using Repository;
using Shared.DTO.Checkout;
using Shared.DTO.Order;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public CheckoutController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IEmailSender emailSender)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("CheckoutOrder/{orderId}")]
        public async Task<IActionResult> CheckoutOrder(Guid orderId)
        {
            try
            {
                // Tìm đơn hàng theo orderId
                var order = await _repository.Order.GetOrderDetailsForPaymentAsync(orderId);
                if(order == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với id {orderId}",
                        Data = null
                    });
                }

                // Tạo một bản ghi Checkout mới
                var checkout = new Checkout
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    PaymentMethodId = order.PaymentMethodId,
                    TotalAmount = order.TotalAmount,
                    LastName = order.LastName,
                    FirstName = order.FirstName,
                    UserName = order.UserName,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    Note = order.Note,
                    PaymentDate = DateTime.Now,
                    OrderStatus = order.OrderStatus,
                    IsSuccessful = true // Thanh toán thành công
                };

                // Lưu bản ghi Checkout vào cơ sở dữ liệu
                await _repository.Checkout.CreateCheckoutAsync(checkout);
                await _repository.Checkout.SaveAsync();

                // Xây dựng nội dung email xác nhận đơn hàng
                var emailBody = _emailSender.BuildOrderConfirmationEmail(order);

                // Tạo đối tượng Message để gửi email
                var message = new Message(new string[] { order.Email }, "Order Confirmation", emailBody);

                // Gửi email xác nhận đơn hàng
                await _emailSender.SendEmailAsync(message);

                //Xóa giỏ hàng khi thanh toán xong
                await _repository.Cart.DeleteCartItemsByUserIdAsync(order.UserId);
                await _repository.Cart.SaveAsync();

                //Xóa đơn hàng
                await _repository.Order.DeleteOrderCheckoutAsync(order.Id);
                await _repository.Order.SaveAsync();


                // Trả về thông tin đơn hàng với giỏ hàng, địa chỉ và phương thức thanh toán, bao gồm thông tin người dùng
                var orderDto = _mapper.Map<OrderDto>(order);

                return Ok(new ApiResponse<OrderDto>
                {
                    Success = true,
                    Message = "Thanh toán thành công và đã lưu vào bảng Checkout.",
                    Data = orderDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside PaymentOrder action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllCheckouts")]
        public async Task<IActionResult> GetAllCheckouts()
        {
            try
            {
                var checkouts = await _repository.Checkout.GetAllCheckoutAsync();
                if(checkouts == null || !checkouts.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Không có Thanh toán nào!",
                        Data = null
                    });
                }

                // Ánh xạ đối tượng Checkout sang CheckoutDto để trả về
                return Ok(new ApiResponse<IEnumerable<CheckoutDto>>
                {
                    Success = true,
                    Message = "Checkouts retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<CheckoutDto>>(checkouts)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCheckouts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllPendingCheckoutOrder")]
        public async Task<IActionResult> GetAllPendingCheckoutOrder()
        {
            try
            {
                var checkouts = await _repository.Checkout.GetAllPendingCheckoutOrdersAsync(trackChanges: false);
                if(checkouts == null || !checkouts.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "No pending orders found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<CheckoutDto>>
                {
                    Success = true,
                    Message = "Pending orders retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<CheckoutDto>>(checkouts)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while getting pending orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllConfirmCheckoutOrder")]
        public async Task<IActionResult> GetAllConfirmCheckoutOrder()
        {
            try
            {
                var checkouts = await _repository.Checkout.GetAllConfirmCheckoutOrdersAsync(trackChanges: false);
                if (checkouts == null || !checkouts.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "No Confirm orders found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<CheckoutDto>>
                {
                    Success = true,
                    Message = "Pending orders retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<CheckoutDto>>(checkouts)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while getting pending orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateCheckoutOrder/{checkoutId}")]
        public async Task<IActionResult> UpdateCheckoutOrder(Guid checkoutId, [FromQuery] UpdateCheckoutDto updateCheckoutDto)
        {
            try
            {
                if (updateCheckoutDto == null)
                {
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Invalid request data.",
                        Data = null
                    });
                }

                var checkout = await _repository.Checkout.GetCheckoutByIdAsync(checkoutId);
                if (checkout == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"No checkout found for order ID {checkoutId}.",
                        Data = null
                    });
                }

                // Cập nhật OrderStatus
                checkout.OrderStatus = updateCheckoutDto.OrderStatus;

                // Lưu thay đổi
                await _repository.Checkout.UpdateCheckoutAsync(checkout);

                // Xây dựng nội dung email xác nhận đơn hàng
                var emailBody = _emailSender.OrderInfomationEmail(checkout);

                // Tạo đối tượng Message để gửi email
                var message = new Message(new string[] { checkout.Email }, "Thông báo", emailBody);

                // Gửi email xác nhận đơn hàng
                await _emailSender.SendEmailAsync(message);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCheckoutPayment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteCheckout/{checkoutId}")]
        public async Task<IActionResult> DeleteCheckout(Guid checkoutId)
        {
            try
            {
                // Kiểm tra thanh toán có tồn tại không
                var checkout = await _repository.Checkout.GetCheckoutByIdAsync(checkoutId);
                if (checkout == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy thông tin thanh toán với ID {checkoutId}.",
                        Data = null
                    });
                }

                //Xóa thanh toán
                await _repository.Checkout.DeleteCheckoutAsync(checkout);

                //Lưu
                await _repository.Checkout.SaveAsync();

                return Ok(new ApiResponse<Object>
                {
                    Success = true,
                    Message = "Thanh toán đã được xóa thành công.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Đã xảy ra lỗi khi xóa thanh toán: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
