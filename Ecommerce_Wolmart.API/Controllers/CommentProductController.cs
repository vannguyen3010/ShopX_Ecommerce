using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.CommentProduct;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CommentProductController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create-CommentProduct")]
        public async Task<IActionResult> CreateCommentProduct([FromBody] CreateCommentProductDto createCommentDto)
        {
            try
            {
                // Kiểm tra dữ liệu nhận được từ client
                if (createCommentDto == null)
                {
                    _logger.LogError("Comment object sent from client is null.");
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Comment object is null.",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid comment object sent from client.");
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Invalid model object.",
                        Data = null
                    });
                }

                //Kiểm tra tồn tại người dùng
                var userExits = await _repository.CommentProduct.GetUserByIdAsync(createCommentDto.UserId);

                if (userExits == null)
                {
                    _logger.LogError($"User with id {createCommentDto.UserId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"User with id {createCommentDto.UserId} not found.",
                        Data = null
                    });
                }


                //Kiểm tra sự tồn tại của sản phẩm
                var productExits = await _repository.Product.GetProductByIdAsync(createCommentDto.ProductId, trackChanges: false);

                if (productExits == null)
                {
                    _logger.LogError($"Product with id {createCommentDto.ProductId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Product with id {createCommentDto.ProductId} not found.",
                        Data = null
                    });
                }

                // Kiểm tra số lượng bình luận gần đây của người dùng
                var recentCommnets = await _repository.CommentProduct.GetRecentCommentsByUserAsync(createCommentDto.UserId, 3);
                var now = DateTime.UtcNow;

                if(recentCommnets.Count() >= 3)
                {
                    var lastCommentTime = recentCommnets.First().CreatedAt;
                    var timeSinceLastComment = now - lastCommentTime;

                    if(timeSinceLastComment.TotalMinutes < 1)
                    {
                        _logger.LogError("Người dùng đang bình luận quá thường xuyên. Vui lòng đợi một lúc trước khi bình luận lại");
                        return BadRequest(new ApiResponse<object>
                        {
                            Success = false,
                            Message = "Người dùng đang bình luận quá thường xuyên. Vui lòng đợi một lúc trước khi bình luận lại.",
                            Data = null
                        });
                    }
                }

                var commentEntity = _mapper.Map<CommentProduct>(createCommentDto);

                //Lưu Bình luận
                await _repository.CommentProduct.CreateCommentAsync(commentEntity);

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Comment created successfully.",
                    Data = _mapper.Map<CommentProductDto>(commentEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCommentProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
