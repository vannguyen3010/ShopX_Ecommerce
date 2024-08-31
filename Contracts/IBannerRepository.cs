using Entities.Models;

namespace Contracts
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllBannersAsync(bool trackChanges);
        Task<Banner> GetBannerByIdAsync(Guid bannerId, bool trackChanges);
        void CreateBanner(Banner banner);
        void UpdateBrand(Banner banner);
        void DeleteBrand(Banner banner);
    }
}
