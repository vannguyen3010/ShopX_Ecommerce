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
        //public string? FileName { get; set; }
        //public string? FileDescription { get; set; }
        public IFormFile? File { get; set; }
    }
  
}
