using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Banner;
using static System.Net.Mime.MediaTypeNames;
using Image = Entities.Models.Image;

namespace Ecommerce_Wolmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IBannerRepository _bannerRepository;
        private readonly IMapper _mapper;

        public BannerController(ILoggerManager logger, IBannerRepository bannerRepository, IMapper mapper)
        {
            _logger = logger;
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
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
                var bannerEntity = await _bannerRepository.CreateBanner(imageDomainModel);
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
        private Entities.Models.BannerPosition MapBannerPosition(Shared.DTO.Banner.BannerPosition position)
        {
            return position switch
            {
                Shared.DTO.Banner.BannerPosition.Top => Entities.Models.BannerPosition.Top,
                Shared.DTO.Banner.BannerPosition.Right => Entities.Models.BannerPosition.Right,
                Shared.DTO.Banner.BannerPosition.Left => Entities.Models.BannerPosition.Left,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };
        }


    }
}
