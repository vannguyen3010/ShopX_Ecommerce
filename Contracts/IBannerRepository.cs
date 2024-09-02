using Entities.Models;

namespace Contracts
{
    public interface IBannerRepository
    {
        Task<Banner> CreateBanner(Banner banner);
        
    }
}
