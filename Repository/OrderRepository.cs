using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly RepositoryContext _dbContext;

        public OrderRepository(RepositoryContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            await _dbContext.Orders.AddAsync(order);
            return order;
        }

        public void DeleteOrderAsync(Order order)
        {
            Delete(order);
        }

        public async Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Order> GetOrderByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
