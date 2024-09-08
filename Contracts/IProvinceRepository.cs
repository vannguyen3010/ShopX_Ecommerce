using Entities.Models.Address;

namespace Contracts
{
    public interface IProvinceRepository
    {
        Task<Province> GetProvinceByCodeAsync(string code);
        Task<IEnumerable<District>> GetDistrictsByProvinceCodeAsync(string provinceCode);
    }
}
