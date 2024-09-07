using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.CateProduct
{
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
