using AutoMapper;
using Azure;
using Contracts;
using Ecommerce_Wolmart.API.Migrations;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Shared.DTO.Address;
using Shared.DTO.Banner;
using Shared.DTO.CateProduct;
using Shared.DTO.Response;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AddressController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateAddress")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDto)
        {
            try
            {
                // Validate Province
                var province = await _repository.Province.GetProvinceByCodeAsync(createAddressDto.ProvinceCode);
                if (province == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Province is null",
                        Data = null
                    });
                }

                // Validate District
                var district = await _repository.District.GetDistrictByCodeAsync(createAddressDto.DistrictCode);
                if (district == null || district.ProvinceCode != createAddressDto.ProvinceCode)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"District is null",
                        Data = null
                    });
                }

                // Validate Ward
                var ward = await _repository.Ward.GetWardByCodeAsync(createAddressDto.WardCode);
                if (ward == null || ward.DistrictCode != createAddressDto.DistrictCode)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"District is null",
                        Data = null
                    });
                }

                // Kiểm tra nếu đia chỉ cụ thể đã tồn tại chưa
                var existingCategory = await _repository.Address.GetAddressByNameAsync(createAddressDto.StreetAddress!);
                if (existingCategory != null)
                {
                    _logger.LogError($"Street Address with name '{createAddressDto.StreetAddress}' already exists.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Street Address with name '{createAddressDto.StreetAddress}' already exists.",
                        Data = null
                    });
                }

                // Ánh xạ Dto sang Enity
                var addressEntities = _mapper.Map<Address>(createAddressDto);

                await _repository.Address.CreateAddressAsync(addressEntities);

                //Ánh xa Entity sang Dto
                var addressDto = _mapper.Map<AddressDto>(addressEntities);

                return Ok(new ApiResponse<AddressDto>
                {
                    Success = true,
                    Message = "Address retrieved successfully.",
                    Data = addressDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside Address action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
           

        }

        [HttpGet]
        [Route("GetAllProvinces")]
        public async Task<IActionResult> GetAllProvinces()
        {
            try
            {
                var provinces = await _repository.Province.GetAllProvinceAsync(trackChanges: false);
                if (provinces == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Returned all provinces from database.",
                        Data = null
                    });
                }

                return Ok(provinces);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProvincesaction: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllDistricts/{provinceCode}")]
        public async Task<IActionResult> GetAllDistricts(string provinceCode)
        {
            try
            {
                var districts = await _repository.District.GetAllDistrictsAsync(provinceCode, trackChanges: false);
                if (districts == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Returned all districts from database.",
                        Data = null
                    });
                }

                return Ok(districts);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAlldistricts: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllWards/{districtCode}")]
        public async Task<IActionResult> GetAllWards(string districtCode)
        {
            try
            {
                var wards = await _repository.Ward.GetAllWardsAsync(districtCode, trackChanges: false);
                if (wards == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Returned all wards from database.",
                        Data = null
                    });
                }

                return Ok(wards);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllwards: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateAdress/{id}")]
        public async Task<IActionResult> UpdateAdress(Guid id, [FromBody] UpdateAddressDto updateAddressDto)
        {
            try
            {
                if (updateAddressDto == null)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Address object is null",
                        Data = null
                    });
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Address object sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Invalid model object",
                        Data = null
                    });
                }


                var existingAddress = await _repository.Address.GetAddressByIdAsync(id, trackChanges: false);
                if (existingAddress == null)
                {
                    _logger.LogError($"Address with id {id} not found.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Address with id {id} not found.",
                        Data = null
                    });
                }

                _mapper.Map(updateAddressDto, existingAddress);

                await _repository.Address.UpdateAddressAsync(existingAddress);
                await _repository.Address.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside UpdateAddress action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            try
            {
                var address = await _repository.Address.GetAllAddressAsync(trackChanges: false);
                if (address == null)
                {
                    _logger.LogError("Address List sent from client is null.");
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"Address List is null",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<IEnumerable<AddressDto>>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<AddressDto>>(address)
                });

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllAddress action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
