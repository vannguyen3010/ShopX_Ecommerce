using Entities.Models;

namespace Contracts
{
    public interface ICateProductsRepository
    {
        Task<CateProduct> CreateCategoryAsync(CateProduct cateProducs);
        Task<(IEnumerable<CateProduct> CateProducts, int Total)> GetAllCategoryProductPagitionAsync(int pageNumber, int pageSize, string? keyword = null);
        Task<IEnumerable<CateProduct>> GetAllCategoryProduct(bool trackChanges);
        Task<CateProduct> GetCategoryProductByIdAsync(Guid id, bool trackChanges);
        Task<CateProduct> GetCategoryByNameAsync(string name);
        Task DeleteCategoryAsync(CateProduct category);
        Task<IEnumerable<CateProduct>> GetChildCategoriesAsync(Guid parentId);
        Task UpdateCategoryAsync(CateProduct cateProduct);
        Task<IEnumerable<CateProduct>> GetChildCategoriesByParentIdAsync(Guid parentcategoryProductId, bool trackChanges);
        Task<bool> HasChildCategoriesAsync(Guid categoryId);
        Task<bool> HasProductsInCategoryAsync(Guid categoryId);
        Task<IEnumerable<CateProduct>> GetAllCategoryProductWithProducts(bool trackChanges);
    }
}
