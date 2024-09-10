using Entities.Models;

namespace Contracts
{
    public interface ICartRepository
    {
        Task AddCartItemAsync(CartItem cartItem);
        Task<bool> SaveAsync();
    }
}
