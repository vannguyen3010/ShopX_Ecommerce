using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.ImageProfile
{
    public class CreateImagePrifileDto
    {
        public string UserId { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
