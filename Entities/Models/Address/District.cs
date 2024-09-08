namespace Entities.Models.Address
{
    public class District
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string FullName { get; set; }
        public string FullNameEn { get; set; }
        public string CodeName { get; set; }

        public string ProvinceCode { get; set; }
        public Province Province { get; set; }

        public int? AdministrativeUnitId { get; set; }
        public AdministrativeUnit AdministrativeUnit { get; set; }

        public ICollection<Ward> Wards { get; set; }
    }
}
