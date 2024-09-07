using Entities.Models;

namespace Contracts
{
    public interface IBannerProductRepository
    {
        Task<BannerProduct> CreateBannerProductAsync(BannerProduct banner);
    }
}
