﻿using AutoMapper;
using Contracts;
using Ecommerce_Wolmart.API.Slug;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryIntroduceController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CategoryIntroduceController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryIntroDto introduce)
        {
            try
            {
                if(introduce == null)
                {
                    _logger.LogError("CategoryIntroduce object sent from client is null.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"CategoryIntroduce object is null",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CategoryIntroduce object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }

                //Kiểm tra tên danh muc đã có chưa
                var existingCategory = await _repository.CategoryIntroduce.GetCategoryIntroduceByNameAsync(introduce.Name!);
                if (existingCategory != null)
                {
                    _logger.LogError($"Name with name '{existingCategory.Name}' already exists.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Name with name '{existingCategory.Name}' already exists.",
                        Data = null
                    });
                }

                var categoryEntity = _mapper.Map<CategoryIntroduce>(introduce);

                // Tạo NameSlug từ Title
                categoryEntity.NameSlug = SlugGenerator.GenerateSlug(introduce.Name);

                // Lưu danh mục vào cơ sở dữ liệu
                await _repository.CategoryIntroduce.CreateCategoryIntroduceAsync(categoryEntity, trackChanges: false);

                return Ok(new ApiResponse<CategoryIntroduceDto>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<CategoryIntroduceDto>(categoryEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var categorys = await _repository.CategoryIntroduce.GetAllCategoryIntroduceAsync(trackChanges: false);
                if (categorys == null)
                {
                    _logger.LogError("Không có danh mục nào!");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không có danh mục nào!",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<CategoryIntroduceDto>>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<CategoryIntroduceDto>>(categorys)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateCategoryIntroduce/{Id}")]
        public async Task<IActionResult> UpdateCategoryIntroduce(Guid Id, [FromBody] UpdateCateIntroDto introduce)
        {
            try
            {
                if (introduce == null)
                {
                    _logger.LogError($"Không tìm thấy {Id} danh mục này!");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy {Id} danh mục này!",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Đối tượng danh mục không hợp lệ.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Đối tượng danh mục không hợp lệ",
                        Data = null
                    });
                }

                var categoryEntity = await _repository.CategoryIntroduce.GetCategoryIntroduceByIdAsync(Id, trackChanges: true);
                if (categoryEntity == null)
                {
                    _logger.LogError($"Danh mục có id: {Id}, không được tìm thấy trong db.");
                    return NotFound();
                }

                _repository.CategoryIntroduce.UpdateCategory(categoryEntity);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteCategoryIntroduce/{Id}")]
        public async Task<IActionResult> DeleteCategoryIntroduce(Guid Id)
        {
            try
            {

                // Kiểm tra xem danh mục có sản phẩm không
                var hasIntroduces = await _repository.CategoryIntroduce.HasIntroducesInCategoryAsync(Id);

                if (hasIntroduces)
                {
                    _logger.LogError($"Không thể xóa danh mục có id {Id} vì nó chứa bài viết.");
                    return BadRequest(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không thể xóa danh mục có id {Id} vì nó chứa bài viết.",
                        Data = null
                    });
                }


                var category = await _repository.CategoryIntroduce.GetCategoryIntroduceByIdAsync(Id, trackChanges: false);
                if (category == null)
                {
                    _logger.LogError($"Không tìm thấy {Id} danh mục này!");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy {Id} danh mục này!",
                        Data = null
                    });
                }
             

                _repository.CategoryIntroduce.DeleteCategory(category);
                _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}