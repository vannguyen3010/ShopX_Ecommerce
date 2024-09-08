using AutoMapper;
using Azure;
using Contracts;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Shared.DTO.Address;
using Shared.DTO.Banner;
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
                var addressEntities = _mapper.Map<Location>(createAddressDto);

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
    }
}
