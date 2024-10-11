using System.Text.Json.Serialization;

namespace Shared.DTO.CateProduct
{
    public class CateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameSlug { get; set; }
        public string FilePath { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<CateProductDto> CategoriesObjs { get; set; } = new List<CateProductDto>();

        //[JsonIgnore]
        public CateProductDto ParentCategory { get; set; }

    }
}
