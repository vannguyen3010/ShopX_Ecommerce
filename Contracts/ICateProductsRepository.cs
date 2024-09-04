using Entities.Models;

namespace Contracts
{
    public interface ICateProductsRepository
    {
        Task<CateProduct> CreateCategoryAsync(CateProduct cateProducs);
        Task<IEnumerable<CateProduct>> GetAllCategoryProduct(bool trackChanges);
        Task<CateProduct> GetCategoryProductByIdAsync(Guid id, bool trackChanges);
        Task<CateProduct> GetCategoryByNameAsync(string name);
        Task DeleteCategoryAsync(CateProduct category);
        Task<IEnumerable<CateProduct>> GetChildCategoriesAsync(Guid parentId);
        Task UpdateCategoryAsync(CateProduct cateProduct);
    }
}
