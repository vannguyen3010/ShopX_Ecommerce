using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        public BannerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateBanner(Banner banner)
        {
            Create(banner);
        }

        public void DeleteBrand(Banner banner)
        {
            Delete(banner);
        }

        public async Task<IEnumerable<Banner>> GetAllBannersAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Banner> GetBannerByIdAsync(Guid bannerId, bool trackChanges)
        {
            return await FindByCondition(brand => brand.Id.Equals(bannerId), trackChanges).FirstOrDefaultAsync();
        }

        public void UpdateBrand(Banner banner)
        {
            Update(banner);
        }
    }
}
