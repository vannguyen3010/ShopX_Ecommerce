using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Product
{
    public class CreateProductDto
    {
        [Required]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public int StockQuantity { get; set; }
        public IFormFile ImageFile { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool IsHot { get; set; }
        
        public List<IFormFile> ImageObjectList { get; set; } = new List<IFormFile>();
    }
  
}
