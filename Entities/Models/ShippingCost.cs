using System.Collections.Generic;

namespace Entities.Models
{
    public class ShippingCost
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProvinceCode { get; set; }
        public decimal Cost { get; set; }
    }
}

