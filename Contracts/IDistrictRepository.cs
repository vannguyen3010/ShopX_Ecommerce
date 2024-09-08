using Entities.Models.Address;

namespace Contracts
{
    public interface IDistrictRepository
    {
        Task<District> GetDistrictByCodeAsync(string code);
        Task<IEnumerable<Ward>> GetWardsByDistrictCodeAsync(string districtCode);
        Task<IEnumerable<District>> GetAllDistrictsAsync(string provinceCode, bool trackChanges);
    }
}
