using Entities.Models;

namespace Contracts
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
       
    }
}
