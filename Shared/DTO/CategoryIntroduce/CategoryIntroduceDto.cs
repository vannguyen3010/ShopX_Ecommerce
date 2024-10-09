using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.CategoryIntroduce
{
    public class CategoryIntroduceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
