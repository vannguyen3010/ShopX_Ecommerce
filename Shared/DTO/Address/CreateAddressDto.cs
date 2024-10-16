namespace Shared.DTO.Address
{
    public class CreateAddressDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string StreetAddress { get; set; }

        // 0: House, 1: Office
        public int AddressType { get; set; }
    }
}
