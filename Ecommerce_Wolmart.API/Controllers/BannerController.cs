using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using Shared;
//using BannerPosition = Entities.Models.BannerPosition;
//using BannerDto = Shared.DTO.Banner.BannerPosition;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BannerController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        #region CreateBanner
        [HttpPost]
        [Route("CreateBanner")]
        public async Task<IActionResult> CreateBanner([FromForm] CreateBannerDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Banner
                {
                    Title = request.Title,
                    Desc = request.Desc,
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,
                    Position = MapBannerPosition(request.Position),
                };

                //User repository to upload image
                var bannerEntity = await _repository.Banner.CreateBanner(imageDomainModel);
                return Ok(_mapper.Map<BannerDto>(bannerEntity));


            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(CreateBannerDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            //// Kiểm tra phần mở rộng tệp
            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }

            //// Kiểm tra kích thước tệp
            if (request.File.Length > 10485760)// Tệp lớn hơn 10MB
            {
                ModelState.AddModelError("File", "file size more than 10MB, please upload a smaller size file .");
            }
        }
        private BannerPosition MapBannerPosition(BannerPosition position)
        {
            return position switch
            {
                BannerPosition.Top => BannerPosition.Top,
                BannerPosition.Right => BannerPosition.Right,
                BannerPosition.Left => BannerPosition.Left,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };
        }
        #endregion

        [HttpGet]
        [Route("GetAllBannerPosition")]
        public async Task<IActionResult> GetAllBannerPosition([FromQuery] BannerPosition position)
        {
            var bannerEntity = await _repository.Banner.GetAllBrandsAsync(position);
            return Ok(bannerEntity);
        }

        [HttpGet]
        [Route("GetBannerById{id}")]
        public async Task<IActionResult> GetBannerById(Guid id)
        {
            try
            {
                var banner = await _repository.Banner.GetBrandByIdAsync(id, trackChanges: false);
                if (banner == null)
                {
                    _logger.LogError($"Brand with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned brand with id: {id}");

                return Ok(_mapper.Map<BannerDto>(banner));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetBrandById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
