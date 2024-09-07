using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.Introduce
{
    public class CreateIntroduceDto
    {
        public string Titlte { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public bool IsHot { get; set; }
    }
}
