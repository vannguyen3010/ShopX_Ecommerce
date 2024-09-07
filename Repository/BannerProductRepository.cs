using Contracts;
using Entities.Models;


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
    }
}
