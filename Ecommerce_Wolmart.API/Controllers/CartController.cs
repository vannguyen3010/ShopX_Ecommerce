using AutoMapper;
using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
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

                // Kiểm tra sản phẩm đã có trong giỏ hàng của người dùng chưa
                var existingCartItem = await _repository.Cart.GetCartItemByProductIdAndUserIdAsync(addToCartDto.ProductId, addToCartDto.UserId);


                if(existingCartItem != null)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, cộng dồn số lượng
                    existingCartItem.Quantity += addToCartDto.Quantity;
                    existingCartItem.Price = product.Price;
                    existingCartItem.Discount = product.Discount;
                    existingCartItem.ImageFilePath = product.ImageFilePath;
                    existingCartItem.ProductName = product.Name;
                    existingCartItem.CategoryName = product.Category != null ? product.Category.Name : "Unknown";

                    _repository.Cart.UpdateCartItem(existingCartItem);

                    // Chỉ cần khôi phục cartItem từ existingCartItem
                    var cartItemDto = _mapper.Map<CartItemDto>(existingCartItem);

                    var result = await _repository.Cart.SaveAsync();

                    if (!result)
                    {
                        _logger.LogError("Error updating item in cart.");
                        return StatusCode(500, "Internal server error");
                    }

                    return Ok(new ApiResponse<CartItemDto>
                    {
                        Success = true,
                        Message = "Sản phẩm đã được cập nhật trong giỏ hàng.",
                        Data = cartItemDto
                    });
                }
                else
                {
                    // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới
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
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Lỗi khi thêm sản phẩm vào giỏ hàng: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetCart/{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            try
            {
                //Kiểm tra userId có tồn tại ko
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogError("UserId is null or empty.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "UserId is required.",
                        Data = null
                    });
                }

                // Lấy danh sách sản phẩm trong giỏ hàng của người dùng
                var cartItems = await _repository.Cart.GetCartItemsByUserIdAsync(userId, trackChanges: false);
                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Cart not found.",
                        Data = null
                    });
                }

                var cartItemDtos = _mapper.Map<IEnumerable<CartItemDto>>(cartItems);

                return Ok(new ApiResponse<IEnumerable<CartItemDto>>
                {
                    Success = true,
                    Message = "Cart retrieved successfully.",
                    Data = cartItemDtos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving cart: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateCartItemQuantity")]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemDto updateCartItemDto)
        {
            try
            {
                if (updateCartItemDto == null)
                {
                    _logger.LogError("UpdateCartItemDto is null.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Invalid input.",
                        Data = null
                    });
                }

                // Kiểm tra sản phẩm đã có trong giỏ hàng của người dùng chưa
                var cartItem = await _repository.Cart.GetCartItemByProductIdAndUserIdAsync(updateCartItemDto.ProductId, updateCartItemDto.UserId);
                if (cartItem == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Sản phẩm không có trong giỏ hàng.",
                        Data = null
                    });
                }

                // Kiểm tra số lượng hợp lệ
                if (updateCartItemDto.Quantity < 1)
                {
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = "Số lượng phải lớn hơn 0.",
                        Data = null
                    });
                }

                // Cập nhật số lượng sản phẩm
                cartItem.Quantity = updateCartItemDto.Quantity;

                // Lưu thay đổi vào cơ sở dữ liệu
                _repository.Cart.UpdateCartItem(cartItem);
                var result = await _repository.Cart.SaveAsync();

                if (!result)
                {
                    _logger.LogError("Error updating item quantity in cart.");
                    return StatusCode(500, "Internal server error");
                }

                var cartItemDto = _mapper.Map<CartItemDto>(cartItem);

                return Ok(new ApiResponse<CartItemDto>
                {
                    Success = true,
                    Message = "Số lượng sản phẩm đã được cập nhật.",
                    Data = cartItemDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Lỗi khi cập nhật số lượng sản phẩm trong giỏ hàng: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
