using Entities.Models;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<IEnumerable<Product>> GetAllProductIsHotAsync();
        Task DeleteProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid id, bool trackChanges);
        Task UpdateProductOrderItemAsync(Product product);
        Task<Product> GetProductByNameAsync(string name);
        Task<(IEnumerable<Product> Products, int Total)> GetAllProductPaginationAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<Product> Products, int Total)> GetProductsByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize);
        Task<IEnumerable<Product>> SearchProductsByNameAsync(string keyword, bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductDiscountAsync(bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductOutByStockStatusAsync(int stockQuantity, bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductOnByStockStatusAsync(int stockQuantity, bool trackChanges);
        Task UpdateProductAsync(Product product, byte[] rowVersion);
        Task SaveAsync();

    }
}
