using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.CategoryIntroduce
{
    public class UpdateCateIntroDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? Name { get; set; }
    }
}
