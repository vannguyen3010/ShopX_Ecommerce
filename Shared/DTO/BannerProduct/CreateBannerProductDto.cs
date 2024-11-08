using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.BannerProduct
{
    public class CreateBannerProductDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public BannerProductWithPopupPosition Position { get; set; }
    }
}
