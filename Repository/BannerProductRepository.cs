using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repository
{
    public class BannerProductRepository : RepositoryBase<BannerProduct>, IBannerProductRepository
    {
        private readonly RepositoryContext _dbContext;

        public BannerProductRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BannerProduct> CreateBannerProductAsync(BannerProduct banner)
        {
            await _dbContext.BannerProducts.AddAsync(banner);
            await _dbContext.SaveChangesAsync();
            return banner;
        }

        public async Task<BannerProduct> GetBannerProductbyId(Guid brandId, bool trackChanges)
        {
            return await FindByCondition(banner => banner.Id.Equals(brandId), trackChanges).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<BannerProduct>> GetAllBannerProductAsync(BannerProductWithPopupPosition? position = null)
        {
            if(position.HasValue)
            {
                return await _dbContext.BannerProducts.Where(x => x.Position == position).ToListAsync();
            }
            else
            {
                return await _dbContext.BannerProducts.ToListAsync();
            }
        }

        public void UpdateBannerProduct(BannerProduct banner)
        {
            Update(banner);
        }
        public void DeleteBannerProduct(BannerProduct banner)
        {
            Delete(banner);
        }

        
    }
}
