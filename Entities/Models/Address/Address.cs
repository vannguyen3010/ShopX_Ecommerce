namespace Entities.Models.Address
{
    public class Address
    {
        public Guid Id { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string StreetAddress { get; set; }
    }
}
