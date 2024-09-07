using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.BannerProduct
{
    public class CreateBannerProduct
    {
        [Required]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
