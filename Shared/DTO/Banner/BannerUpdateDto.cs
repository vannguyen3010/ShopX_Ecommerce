using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Banner
{
    public class BannerUpdateDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public BannerPosition Position { get; set; }

        public IFormFile? SecondFile { get; set; }
    }
    public class BannerUpdateStatusDto
    {
        public bool Status { get; set; }
    }
}
