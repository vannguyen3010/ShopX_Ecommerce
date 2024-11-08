using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.BannerProduct
{
    public class UpdateBannerProductDto
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile? File { get; set; }
        public BannerProductWithPopupPosition Position { get; set; }
    }
    public class UpdateBannerProductStatusDto
    {
        public bool Status { get; set; }
    }
}
