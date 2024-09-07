using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Introduce
{
    public class UpdateIntroduceDto
    {
        [Required]
        public string Titlte { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public bool IsHot { get; set; }
    }
}
