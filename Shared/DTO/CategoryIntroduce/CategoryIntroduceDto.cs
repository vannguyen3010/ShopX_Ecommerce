using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.CategoryIntroduce
{
    public class CategoryIntroduceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DateTime { get; set; }
    }
}
