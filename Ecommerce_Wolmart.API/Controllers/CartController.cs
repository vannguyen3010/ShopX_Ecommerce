using AutoMapper;
using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Cart;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CartController(UserManager<User> userManager, ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {

                if (addToCartDto == null)
                {
                    _logger.LogError("AddToCartDto is null.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Invalid input.",
                        Data = null
                    });
                }

                // Kiểm tra sản phẩm có tồn tại không
                var product = await _repository.Product.GetProductByIdAsync(addToCartDto.ProductId, trackChanges: false);
                if (product == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Sản phẩm không tồn tại.",
                        Data = null
                    });
                }

                // Kiểm tra người dùng có tồn tại không
                //var user = await _repository.CommentProduct.GetUserByIdAsync(addToCartDto.UserId);
                var user = await _userManager.FindByIdAsync(addToCartDto.UserId);
                if (user == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Người dùng này không tồn tại.",
                        Data = null
                    });
                }

                // Thêm sản phẩm vào giỏ hàng
                var cartItem = _mapper.Map<CartItem>(new CartItemDto
                {
                    Id = Guid.NewGuid(),
                    UserId = addToCartDto.UserId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    Price = product.Price,
                    Discount = product.Discount,
                    ImageFilePath = product.ImageFilePath,
                    ProductName = product.Name,
                    CategoryName = product.CategoryName != null ? product.CategoryName : "Unknown",// Đảm bảo lấy CategoryName từ Product
                });


                if (cartItem == null)
                {
                    _logger.LogError("Mapping CartItemDto to CartItem failed.");
                    return StatusCode(500, "Internal server error");
                }

                cartItem.Id = Guid.NewGuid(); // Set a new unique ID
                cartItem.UserId = addToCartDto.UserId; // Set UserId

                await _repository.Cart.AddCartItemAsync(cartItem);
                var result = await _repository.Cart.SaveAsync();

                if (!result)
                {
                    _logger.LogError("Error adding item to cart.");
                    return StatusCode(500, "Internal server error");
                }

                var cartItemDto = _mapper.Map<CartItemDto>(cartItem);

                return Ok(new ApiResponse<CartItemDto>
                {
                    Success = true,
                    Message = "Sản phẩm đã được thêm vào giỏ hàng.",
                    Data = cartItemDto
                });

            }
            catch (Exception ex)
            {

                _logger.LogError($"Lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
