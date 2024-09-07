using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BannerProduct>> GetAllBannerProductAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<BannerProduct> GetBannerProductbyId(Guid brandId, bool trackChanges)
        {
            return await FindByCondition(banner => banner.Id.Equals(brandId), trackChanges).FirstOrDefaultAsync();
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
