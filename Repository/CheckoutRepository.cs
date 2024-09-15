using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CheckoutRepository : RepositoryBase<Checkout>, ICheckoutRepository
    {
        private readonly RepositoryContext _dbContext;

        public CheckoutRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCheckoutAsync(Checkout checkout)
        {
            await _dbContext.Checkouts.AddAsync(checkout);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Checkout>> GetAllCheckoutAsync()
        {
            return await _dbContext.Checkouts.ToListAsync();
        }

        public async Task<IEnumerable<Checkout>> GetAllConfirmCheckoutOrdersAsync(bool trackChanges)
        {
            return await _dbContext.Checkouts
                .Where(x => x.OrderStatus == true).ToListAsync();
        }

        public async Task<IEnumerable<Checkout>> GetAllPendingCheckoutOrdersAsync(bool trackChanges)
        {
            return await _dbContext.Checkouts
                .Where(x => x.OrderStatus == false).ToListAsync();
        }
        public async Task UpdateCheckoutAsync(Checkout checkout)
        {
            _dbContext.Checkouts.Update(checkout);
            await SaveAsync();
        }

        public async Task<Checkout> GetCheckoutByIdAsync(Guid checkoutId)
        {
            return await _dbContext.Checkouts.FirstOrDefaultAsync(x => x.Id == checkoutId);
        }
        public async Task DeleteCheckoutAsync(Checkout checkout)
        {
            Delete(checkout);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

      
    }
}
