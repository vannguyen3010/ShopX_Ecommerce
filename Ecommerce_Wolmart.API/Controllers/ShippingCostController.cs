using AutoMapper;
using Contracts;
using Entities.Models.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Contact;
using Shared.DTO.Product;
using Shared.DTO.Response;
using Shared.DTO.ShippingCost;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingCostController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ShippingCostController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetShippingCostByAddressId/{addressId}")]
        public async Task<IActionResult> GetShippingCostByAddressId(Guid addressId)
        {
            //Lấy id địa chỉ
            var address = await _repository.Address.GetAddressByIdAsync(addressId, trackChanges: false);
            if (address == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Không tìm thấy địa chỉ này {addressId}",
                    Data = null
                });
            }

            // Lấy ProvinceCode từ địa chỉ
            string provinceCode = address.ProvinceCode;
            string provinceName = address.ProvinceName;

            //Lấy phí vận chuyển dựa trên ProvinceCode 
            var shippingCost = await _repository.ShippingCost.GetShippingCostByProvinceAsync(provinceCode);
            if (shippingCost == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Không tìm thấy chi phí vận chuyển {provinceCode}",
                    Data = null
                });
            }
            var shippingCostDto = new ShippingCostDto
            {
                Id = shippingCost.Id,
                ProvinceCode = shippingCost.ProvinceCode,
                ProvinceName = provinceName, // Thêm ProvinceName
                Cost = shippingCost.Cost
            };
            //Chuyển đổi model thành DTO
            return Ok(new ApiResponse<ShippingCostDto>
            {
                Success = true,
                Message = "Address retrieved successfully.",
                Data = shippingCostDto
            });
        }

        [HttpGet]
        [Route("GetShippingCostById/{Id}")]
        public async Task<IActionResult> GetShippingCostById(Guid Id)
        {
            //Lấy id địa chỉ
            var shippingCost = await _repository.ShippingCost.GetShippingCostByIdAsync(Id, trackChanges: false);
            if (shippingCost == null)
            {
                return NotFound(new ApiResponse<Object>
                {
                    Success = false,
                    Message = $"Không tìm thấy địa chỉ này {Id}",
                    Data = null
                });
            }

            var shippingCostDto = _mapper.Map<ShippingCostDto>(shippingCost);
            //Chuyển đổi model thành DTO
            return Ok(new ApiResponse<ShippingCostDto>
            {
                Success = true,
                Message = "Address retrieved successfully.",
                Data = shippingCostDto
            });
        }

        [HttpPut]
        [Route("UpdateShippingCost/{Id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateShippingCost(Guid Id, [FromQuery] UpdateCostDto updateCostDto)
        {
            try
            {
                if (updateCostDto == null)
                {
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy id này {Id}",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ShippingCost object sent from client.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object.",
                        Data = null
                    });
                }

                var costEntity = await _repository.ShippingCost.GetShippingCostByIdAsync(Id, trackChanges: true);
                if (costEntity == null)
                {
                    _logger.LogError($"ShippingCost with id: {Id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Invalid model object.",
                        Data = null
                    });
                }

                _mapper.Map(updateCostDto, costEntity);

                _repository.ShippingCost.UpdateShippingCost(costEntity);
                _repository.SaveAsync();

                return Ok(_mapper.Map<ShippingCostDto>(costEntity));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ShippingCost action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllShippingCost")]
        public async Task<IActionResult> GetAllShippingCost()
        {
            try
            {
                var shippingCosts = await _repository.ShippingCost.GetAllShippingCostAsync(trackChanges: false);

                if (shippingCosts == null)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy phí vận chuyển",
                        Data = null
                    });
                }

                // Ánh xạ từ entity sang DTO nếu cần thiết
                var shippingCostsDto = _mapper.Map<IEnumerable<ShippingCostDto>>(shippingCosts);

                foreach (var shippingCost in shippingCostsDto)
                {
                    var province = await _repository.Province.GetProvinceByCodeAsync(shippingCost.ProvinceCode);
                    var shippingCostDto = _mapper.Map<ShippingCostDto>(shippingCost);

                    // Nếu tìm thấy Province, thêm tên tỉnh thành vào DTO
                    if(province != null)
                    {
                        shippingCostDto.ProvinceName = province.Name;
                    }
                }

                return Ok(new ApiResponse<IEnumerable<ShippingCostDto>>
                {
                    Success = true,
                    Message = "Danh sách phí vận chuyển !.",
                    Data = shippingCostsDto
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProductIsHot action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetAllShippingCostPagination")]
        public async Task<IActionResult> GetAllShippingCostPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, string? keyword = null)
        {
            try
            {
                var (shippingCosts, totalCount) = await _repository.ShippingCost.GetAllShippingCostPaginationAsync(pageNumber, pageSize, keyword);

                if (!shippingCosts.Any())
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Không tìm thấy phí vận chuyển",
                        Data = null
                    });
                }

                var shippingCostsDto = _mapper.Map<IEnumerable<ShippingCostDto>>(shippingCosts);

                return Ok(new
                {
                    success = true,
                    message = "Shipping retrieved successfully.",
                    data = new
                    {
                        totalCount,
                        shippingCosts = shippingCostsDto
                    }
                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetAllProductIsHot action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateStatusShippingCost/{Id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateStatusShippingCost(Guid Id, [FromQuery] UpdateStatusCostDto updateCostDto)
        {
            try
            {
                var costEntity = await _repository.ShippingCost.GetShippingCostByIdAsync(Id, trackChanges: true);
                if (costEntity == null)
                {
                    _logger.LogError($"ShippingCost with id: {Id}, hasn't been found in db.");
                    return NotFound(new ApiResponse<Object>
                    {
                        Success = false,
                        Message = $"Không tìm thấy {Id}.",
                        Data = null
                    });
                }

                _mapper.Map(updateCostDto, costEntity);

                _repository.ShippingCost.UpdateShippingCost(costEntity);
                _repository.SaveAsync();

                return Ok(_mapper.Map<ShippingCostDto>(costEntity));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside ShippingCost action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
