using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CartRepository : RepositoryBase<CartItem>, ICartRepository
    {
        private readonly RepositoryContext _dbContext;

        public CartRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);
        }

        public async Task<CartItem> GetCartItemByProductIdAndUserIdAsync(Guid productId, string userId)
        {
            return await _dbContext.CartItems
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId, bool trackChanges)
        {
            return await _dbContext.CartItems
                .Where(x => x.UserId == userId)
                .Include(x => x.Product)// Đảm bảo lấy thông tin sản phẩm
                .ThenInclude(x => x.Category)// Lấy thông tin danh mục sản phẩm
                .ToListAsync();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _dbContext.CartItems.Remove(cartItem);
        }

        public async Task<bool> SaveAsync()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }
    }
}
