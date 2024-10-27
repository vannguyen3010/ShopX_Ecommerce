using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class CategoryIntroduce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public bool Status { get; set; }

        [ForeignKey("ParentCategory")]
        public Guid? ParentCategoryId { get; set; }
        public CategoryIntroduce ParentCategory { get; set; }
        public ICollection<Introduce> Introduces { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
