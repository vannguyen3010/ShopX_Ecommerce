namespace Shared.DTO.ShippingCost
{
    public class ShippingCostDto
    {
        public Guid Id { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public decimal Cost { get; set; }
    }
}
