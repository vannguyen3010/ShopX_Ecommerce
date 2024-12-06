﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.CommentProduct;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Route("CreateCommentProduct")]
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
                var user = await _repository.CommentProduct.GetUserByIdAsync(createCommentDto.UserId);

                if (user == null)
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
                var product = await _repository.Product.GetProductByIdAsync(createCommentDto.ProductId, trackChanges: false);
                if (product == null)
                {
                    _logger.LogError($"Product with id {createCommentDto.ProductId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Product with id {createCommentDto.ProductId} not found.",
                        Data = null
                    });
                }

                // Kiểm tra xem người dùng có được phép bình luận không
                var lastComment = await _repository.CommentProduct.GetLastCommentByUserAndProductAsync(createCommentDto.UserId, createCommentDto.ProductId);

                // Kiểm tra số lượng bình luận và thời gian gần nhất
                if (lastComment != null)
                {
                    var timeSinceLastComment = DateTime.UtcNow - lastComment.CreatedAt;
                    if (timeSinceLastComment.TotalMinutes < 1)
                    {
                        _logger.LogError("Người dùng không thể bình luận lại trong vòng 1 phút trên cùng một sản phẩm.");
                        return NotFound(new ApiResponse<object>
                        {
                            Success = false,
                            Message = $"Người dùng không thể bình luận lại trong vòng 1 phút trên cùng một sản phẩm.",
                            Data = null
                        });
                    }
                }

                var commentEntity = _mapper.Map<CommentProduct>(createCommentDto);

                commentEntity.CreatedAt = DateTime.UtcNow;
                commentEntity.ProductName = product.Name;
                commentEntity.UserName = user.UserName!;

                //Lưu Bình luận
                await _repository.CommentProduct.CreateCommentAsync(commentEntity);


                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Bình luận đã được tạo thành công.",
                    Data = _mapper.Map<CommentProductDto>(commentEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCommentProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetCommentProductById/{commentId}")]
        public async Task<IActionResult> GetCommentProductById(Guid commentId)
        {
            try
            {
                // Kiểm tra xem bình luận có tồn tại không
                var comment = await _repository.CommentProduct.GetCommentByIdAsync(commentId, trackChanges: false);

                if (comment == null)
                {
                    _logger.LogError($"Comment with id {commentId} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Comment with id {commentId} not found.",
                        Data = null
                    });
                }
                // Chuyển đổi bình luận sang DTO
                var commentDto = _mapper.Map<CommentProductDto>(comment);
                return Ok(new ApiResponse<CommentProductDto>
                {
                    Success = true,
                    Message = "Comment retrieved successfully.",
                    Data = commentDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetCommentProductById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllCommentProductByProductId/{productId}")]
        public async Task<IActionResult> GetAllCommentProductByProductId(Guid productId)
        {
            try
            {
                var comments = await _repository.CommentProduct.GetAllCommentsByProductIdAsync(productId, trackChanges: false);
                if (comments == null || !comments.Any())
                {
                    _logger.LogError($"Không tìm thấy bình luận nào trong sản phẩm id {productId}.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy bình luận nào trong sản phẩm id {productId}.",
                        Data = null
                    });
                }

                var commentDtos = comments.Select(comment =>
                {
                    var commentDto = _mapper.Map<CommentProductDto>(comment);
                    commentDto.UserName = comment.User?.UserName;
                    commentDto.ProductName = comment.Product?.Name;
                    return commentDto;
                });

                return Ok(new ApiResponse<IEnumerable<CommentProductDto>>
                {
                    Success = true,
                    Message = "Comments retrieved successfully.",
                    Data = commentDtos
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllCommentProductByProductId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllCommentProduct")]
        public async Task<IActionResult> GetAllCommentProduct()
        {
            try
            {
                var comments = await _repository.CommentProduct.GetAllCommentsAsync(trackChanges: false);
                if (comments == null || !comments.Any())
                {
                    _logger.LogError("No comments found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No comments found.",
                        Data = null
                    });
                }

                var commentDtos = comments.Select(comment =>
                {
                    var commentDto = _mapper.Map<CommentProductDto>(comment);
                    commentDto.UserName = comment.User?.UserName;
                    commentDto.ProductName = comment.Product?.Name;

                    return commentDto;

                });

                return Ok(new ApiResponse<IEnumerable<CommentProductDto>>
                {
                    Success = true,
                    Message = "Comments retrieved successfully.",
                    Data = commentDtos
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllComment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                // Kiểm tra bình luận có tồn tại không
                var comment = await _repository.CommentProduct.GetCommentByIdAsync(commentId, trackChanges: false);
                if (comment == null)
                {
                    _logger.LogError($"Comment with id: {commentId} doesn't exist.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Comment not found.",
                        Data = null
                    });
                }

                // Kiểm tra quyền hạn (ví dụ: chỉ cho phép người tạo bình luận hoặc admin xóa)
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Lấy ID người dùng từ JWT token
                //if (comment.UserId != userId && !User.IsInRole("Admin"))
                //{
                //    _logger.LogError("Unauthorized access attempt to delete comment.");
                //    return Forbid("You don't have permission to delete this comment.");
                //}

                // Thực hiện xóa bình luận
                _repository.CommentProduct.DeleteComment(comment);
                await _repository.CommentProduct.SaveAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Comment deleted successfully.",
                    Data = null
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCommentProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
