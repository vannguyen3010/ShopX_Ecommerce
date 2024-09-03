using Entities.Models;

namespace Contracts
{
    public interface ICateProductsRepository
    {
        Task<CateProduct> CreateCategoryAsync(CateProduct cateProducs);
    }
}
