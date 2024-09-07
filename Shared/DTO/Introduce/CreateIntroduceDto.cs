using Microsoft.AspNetCore.Http;

namespace Shared.DTO.Introduce
{
    public class CreateIntroduceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public bool IsHot { get; set; }
    }
}
