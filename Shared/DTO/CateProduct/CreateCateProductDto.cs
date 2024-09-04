using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Category
{
    public class CreateCateProductDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
    public class CateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ICollection<CateProductDto> CategoriesObjs { get; set; } = new List<CateProductDto>();
        public DateTime CreateAt { get; set; }
        public DateTime DateTime { get; set; }

    }
    public class UpdateCateProductDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Name { get; set; }
        //public Guid? ParentCategoryId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
