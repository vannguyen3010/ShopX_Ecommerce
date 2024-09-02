using Entities.Models;
using Shared;

namespace Contracts
{
    public interface IBannerRepository
    {
        Task<Banner> CreateBanner(Banner banner);
        Task<IEnumerable<Banner>> GetAllBrandsAsync(BannerPosition position);
    }
}
