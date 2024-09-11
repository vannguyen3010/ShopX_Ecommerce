using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class CategoryIntroduce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }

        [ForeignKey("ParentCategory")]
        public Guid? ParentCategoryId { get; set; }
        public CategoryIntroduce ParentCategory { get; set; }
        public ICollection<Introduce> Introduces { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DateTime { get; set; }
    }
}
