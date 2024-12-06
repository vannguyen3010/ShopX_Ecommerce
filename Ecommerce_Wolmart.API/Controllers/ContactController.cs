using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.DTO.Banner;
using Shared.DTO.Contact;
using Shared.DTO.Product;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ContactController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, IMemoryCache cache)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpPost]
        [Route("CreateContact")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto contactDto)
        {
            try
            {
                if(contactDto == null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if(!ModelState.IsValid)
                {

                    _logger.LogError("Invalid brand object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var contactEntity = _mapper.Map<Contact>(contactDto);

                contactEntity.Status = false;

                await _repository.Contact.CreateContactAsync(contactEntity);

                return Ok(new ApiResponse<ContactDto>
                {
                    Success = true,
                    Message = "Banner created successfully.",
                    Data = _mapper.Map<ContactDto>(contactEntity)
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside CreateBrand action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetContactById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            try
            {
                string cacheKey = $"Contact_{id}";

                // Kiểm tra xem cache có tồn tại với key này không
                if (!_cache.TryGetValue(cacheKey, out ContactDto cachedContact))
                {
                    var contact = await _repository.Contact.GetContactByIdAsync(id, trackChanges: false);
                    if (contact == null)
                    {
                        _logger.LogError($"Contact with id: {id}, hasn't been found in db.");
                        return NotFound(new ApiResponse<ContactDto>
                        {
                            Success = false,
                            Message = "Contact not found.",
                            Data = null
                        });
                    }

                    cachedContact = _mapper.Map<ContactDto>(contact);

                    // Cấu hình và lưu dữ liệu vào cache
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10)) // Gia hạn cache nếu được truy cập trong khoảng thời gian này
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));  // Cache hết hạn sau 1 giờ

                    _cache.Set(cacheKey, cachedContact, cacheOptions);
                }
                else
                {
                    _logger.LogInfo($"Cache hit for contact with id: {id}");
                }

                return Ok(new ApiResponse<ContactDto>
                {
                    Success = true,
                    Message = "Banner created successfully.",
                    Data = cachedContact
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetBrandById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllContact")]
        [Authorize]
        public async Task<IActionResult> GetAllContact(int type = 0, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (contacts, totalCount) = await _repository.Contact.GetAllContactAsync(trackChanges: false, type: type, pageNumber, pageSize);
                _logger.LogInfo("Returned all brands from database.");

                var contactsResult = _mapper.Map<IEnumerable<ContactDto>>(contacts);
                return Ok(new
                {
                    Success = true,
                    Message = "Banner created successfully.",
                    data = new
                    {
                        totalCount,
                        contacts = contactsResult
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllBrands action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(Guid id) 
        {
            try
            {
                var contact = await _repository.Contact.GetContactByIdAsync(id, trackChanges: false);
                if (contact == null)
                {
                    _logger.LogError($"Brand with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Contact.DeleteContact(contact);
                _repository.SaveAsync();

                return Ok(new ApiResponse<ContactDto>
                {
                    Success = true,
                    Message = "Banner created successfully.",
                    Data = _mapper.Map<ContactDto>(contact)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBrand action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromQuery] UpdateContactDto contactDto)
        {
            try
            {
                if (contactDto == null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Contact object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var contactEntity = await _repository.Contact.GetContactByIdAsync(id, trackChanges: true);
                if (contactEntity == null)
                {
                    _logger.LogError($"Contact with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(contactDto, contactEntity);

                _repository.Contact.UpdateContact(contactEntity);
                _repository.SaveAsync();

                return NoContent();
                //return Ok(_mapper.Map<ContactDto>(contactEntity));
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
