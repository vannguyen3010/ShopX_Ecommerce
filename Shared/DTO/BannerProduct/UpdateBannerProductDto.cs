using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.BannerProduct
{
    public class UpdateBannerProductDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
