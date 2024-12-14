using Entities.Models;

namespace Contracts
{
    public interface IShippingCostRepository
    {
        Task<ShippingCost> GetShippingCostByProvinceAsync(string provinceCode);
        Task<ShippingCost> GetShippingCostByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<ShippingCost>> GetAllShippingCostAsync(bool trackChanges);
        Task<(IEnumerable<ShippingCost>, int totalCount)> GetAllShippingCostPaginationAsync(int pageNumber, int pageSize, string? keyword = null);
        Task<ShippingCost> GetShippingCostByProvinceCodeAsync(string provinceCode);
        void UpdateShippingCost(ShippingCost shippingCost);
    }
}
