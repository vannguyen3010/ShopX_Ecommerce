using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Banner
{
    public class CreateBannerDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public BannerPosition Position { get; set; }
        public IFormFile File { get; set; }
        public IFormFile? SecondFile { get; set; }
    }
}
