using Entities.Models;
using Shared;

namespace Contracts
{
    public interface IBannerRepository
    {
        Task<Banner> CreateBanner(Banner banner);
        Task<IEnumerable<Banner>> GetAllBannersAsync(BannerPosition? position = null);
        Task<Banner> GetBannerByIdAsync(Guid brandId, bool trackChanges);
        Task UpdateBanner(Banner banner);
        Task DeleteBanner(Guid id);
    }
}
