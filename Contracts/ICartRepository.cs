using Entities.Models;

namespace Contracts
{
    public interface ICartRepository
    {
        Task AddCartItemAsync(CartItem cartItem);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId, bool trackChanges);
        Task<CartItem> GetCartItemByProductIdAndUserIdAsync(Guid productId, string userId);
        void UpdateCartItem(CartItem cartItem);
        void DeleteCartItem(CartItem cartItem);
        Task<bool> SaveAsync();
    }
}
