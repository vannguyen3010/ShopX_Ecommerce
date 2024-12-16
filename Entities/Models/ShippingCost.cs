using System.Collections.Generic;

namespace Entities.Models
{
    public class ShippingCost
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public decimal Cost { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

