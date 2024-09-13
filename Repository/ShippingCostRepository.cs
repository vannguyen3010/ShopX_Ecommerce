using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ShippingCostRepository : RepositoryBase<ShippingCost>, IShippingCostRepository
    {
        private readonly RepositoryContext _dbContext;

        public ShippingCostRepository(RepositoryContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShippingCost> GetShippingCostByProvinceAsync(string provinceCode)
        {
            return await _dbContext.ShippingCosts
                    .FirstOrDefaultAsync(x => x.ProvinceCode == provinceCode);
        }
        public async Task<ShippingCost> GetShippingCostByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShippingCost>> GetAllShippingCostAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.ProvinceCode).ToListAsync();
        }

        public void UpdateShippingCost(ShippingCost shippingCost)
        {
            Update(shippingCost);
        }

        public async Task<ShippingCost> GetShippingCostByProvinceCodeAsync(string provinceCode)
        {
            return await _dbContext.ShippingCosts
                .FirstOrDefaultAsync(x => x.ProvinceCode == provinceCode);
        }
    }
}
