namespace Shared.DTO.Address
{
    public class CreateAddressDto
    {
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string StreetAddress { get; set; }
    }
}
