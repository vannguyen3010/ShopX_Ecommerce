using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Introduce
{
    public class UpdateIntroduceDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile? File { get; set; }
        public bool IsHot { get; set; }
    }
}
