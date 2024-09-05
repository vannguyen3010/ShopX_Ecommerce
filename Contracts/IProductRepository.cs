using Entities.Models;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task DeleteProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid id, bool trackChanges);
        Task UpdateProductAsync(Product product);
    }
}
