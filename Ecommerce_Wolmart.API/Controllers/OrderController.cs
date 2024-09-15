using AutoMapper;
using Azure.Core;
using Contracts;
using EmailService;
using Entities.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository;
using Shared.DTO.Address;
using Shared.DTO.Cart;
using Shared.DTO.Contact;
using Shared.DTO.Order;
using Shared.DTO.Payments;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public OrderController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IEmailSender emailSender)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                if (createOrderDto == null)
                {
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Invalid request data..",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CreateOrder object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                // Kiểm tra xem UserId đã có đơn hàng chưa
                var existingOrder = await _repository.Order.GetOrderByUserIdAsync(createOrderDto.UserId.ToString());
                if (existingOrder != null)
                {
                    return Conflict(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Đơn hàng đã tồn tại cho người dùng với ID {createOrderDto.UserId}.",
                        Data = null
                    });
                }

                // Lấy thông tin địa chỉ
                var address = await _repository.Address.GetAddressByIdAsync(createOrderDto.AddressId, trackChanges: false);
                if (address == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy địa chỉ của {createOrderDto.AddressId}!",
                        Data = null
                    });
                }

                // Lấy phí vận chuyển dựa trên mã tỉnh
                var shippingCost = await _repository.ShippingCost.GetShippingCostByProvinceCodeAsync(address.ProvinceCode);
                if (shippingCost == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm phí ship của !",
                        Data = null
                    });
                }

                // Lấy danh sách sản phẩm trong giỏ hàng
                var cartItems = await _repository.Cart.GetCartItemsByUserIdAsync(createOrderDto.UserId, trackChanges: false);
                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy giỏ hàng của người dùng này {createOrderDto.UserId}!",
                        Data = null
                    });
                }

                // Tính toán tổng giá trị và tổng số tiền
                var totalDiscount = cartItems.Sum(item => item.Discount);
                var totalPrice = cartItems.Sum(item => item.Price);

                // Tạo đợn hàng mới
                var order = _mapper.Map<Order>(createOrderDto);

                // Cập nhật thông tin bổ sung cho Order
                order.ShippingCostId = shippingCost.Id;
                order.ShippingCost = shippingCost.Cost;
                order.Discount = totalDiscount;
                order.Price = totalPrice;
                order.TotalAmount = totalPrice - totalDiscount + shippingCost.Cost;
                order.OrderDate = DateTime.Now;
                order.OrderStatus = false; // Default value
                order.CartItems = cartItems.ToList();


                // Save the order
                var createdOrder = await _repository.Order.CreateOrderAsync(order);
                await _repository.Order.SaveAsync();

                // Ánh xạ đối tượng Order sang OrderDto để trả về
                var orderDto = _mapper.Map<OrderDto>(order);

                return Ok(new ApiResponse<OrderDto>
                {
                    Success = true,
                    Message = "Order created successfully.",
                    Data = orderDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside BannerProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateOrderStatus/{Id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid Id, [FromQuery] UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _repository.Order.GetOrderByIdAsync(Id, trackChanges: false);
                if (order == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Order with id {Id} not found.",
                        Data = null
                    });
                }

                // Cập nhật trạng thái đơn hàng
                order.OrderStatus = updateOrderDto.OrderStatus; // Cập nhật thành đã duyệt (true tương đương với 1)

                _repository.Order.UpdateOrderAsync(order);
                await _repository.Order.SaveAsync();

               return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating order status: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetPending-Orders")]
        public async Task<IActionResult> GetPendingOrders()
        {
            try
            {
                var orders = await _repository.Order.GetPendingOrdersAsync(trackChanges: false);
                if (orders == null || !orders.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "No pending orders found.",
                        Data = null
                    });
                }

                var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
                return Ok(new ApiResponse<IEnumerable<OrderDto>>
                {
                    Success = true,
                    Message = "Pending orders retrieved successfully.",
                    Data = ordersDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while getting pending orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteOrder/{Id}")]
        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
            try
            {
                var order = await _repository.Order.GetOrderByIdAsync(Id, trackChanges: false);
                if (order == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy đơn hàng với ID {Id}!",
                        Data = null
                    });
                }

                // Kiểm tra xem có sản phẩm nào trong giỏ hàng của người dùng hay không
                var cartItems = await _repository.Cart.GetCartItemsByUserIdAsync(order.UserId, trackChanges: false);
                if (cartItems != null && cartItems.Any())
                {
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Không thể xóa đơn hàng vì giỏ hàng của người dùng vẫn còn sản phẩm.",
                        Data = null
                    });
                }

                _repository.Order.DeleteOrderAsync(order);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBrand action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
