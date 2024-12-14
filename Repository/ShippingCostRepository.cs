using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Repository
{
    public class ShippingCostRepository : RepositoryBase<ShippingCost>, IShippingCostRepository
    {
        private readonly RepositoryContext _dbContext;

        public ShippingCostRepository(RepositoryContext dbContext) : base(dbContext)
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

        public async Task<(IEnumerable<ShippingCost>, int totalCount)> GetAllShippingCostPaginationAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            var query = _dbContext.ShippingCosts.AsQueryable();

            //Lọc theo keyword nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerCaseName = keyword.ToLower();

                query = query.Where(x => x.ProvinceName.ToLower().Contains(lowerCaseName));
            }

            var totalCount = await query.CountAsync();

            // Phân trang
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }
    }
}
