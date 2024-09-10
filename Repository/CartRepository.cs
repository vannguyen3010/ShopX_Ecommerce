using Contracts;
using Entities.Models;

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

        public async Task<bool> SaveAsync()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }
    }
}
