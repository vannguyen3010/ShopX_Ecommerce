using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Category;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CategoryController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost("CreateCatogory")]
        public IActionResult CreateCatogory([FromBody] CreateCategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    _logger.LogError("Category object sent from client is null.");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = _mapper.Map<Category>(categoryDto);

                _repository.Category.CreateCategoryAsync(categoryEntity);
                _repository.SaveAsync();

                //var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

                //return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
                return Ok(_mapper.Map<CategoryDto>(categoryEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
