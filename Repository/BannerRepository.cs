using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repository
{
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        private readonly RepositoryContext _dbContext;

        public BannerRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Banner> CreateBanner(Banner banner)
        {
            await _dbContext.Banners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();
            return banner;
        }
        public async Task<IEnumerable<Banner>> GetAllBannersAsync(BannerPosition? position = null)
        {
            if(position.HasValue)
            {
                return await _dbContext.Banners.Where(x => x.Position == position).ToListAsync();
            }
            else
            {
                return await _dbContext.Banners.ToListAsync();
            }
        }
        public async Task<Banner> GetBannerByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }
        public async Task UpdateBanner(Banner banner)
        {
            _dbContext.Banners.Update(banner);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBanner(Guid id)
        {
            var banner = await _dbContext.Banners.FindAsync(id);
            if(banner != null)
            {
                _dbContext.Banners.Remove(banner);
                await _dbContext.SaveChangesAsync();
            }
        }

      
    }
}
