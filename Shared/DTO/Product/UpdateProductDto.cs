using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public Guid CategoryId { get; set; }
        public IFormFile? File { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool IsHot { get; set; }
    }
}
