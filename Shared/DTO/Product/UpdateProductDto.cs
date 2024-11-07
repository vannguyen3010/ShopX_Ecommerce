using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Product
{
    public class UpdateProductDto
    {
        //public string? RowVersion { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool IsHot { get; set; }
        public int StockQuantity { get; set; }

        public IFormFile? File { get; set; }
        public List<IFormFile> ImageObjectList { get; set; } = new List<IFormFile>();
    }

    public class UpdateProductStatusDto
    {
        public bool Status { get; set; }
    }
}
