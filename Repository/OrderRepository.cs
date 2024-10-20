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

        public async Task<Order> GetOrderByIdAsync(Guid orderId, bool trackChanges)
        {
            return await FindByCondition(order => order.Id.Equals(orderId), trackChanges)
                .Include(order => order.OrderItems)
                .Include(order => order.Address)
                .Include(order => order.PaymentMethod)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderByUserIdAsync(string userId)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Order> GetOrderDetailsForPaymentAsync(Guid orderId)
        {
            return await _dbContext.Orders
             .Include(o => o.CartItems)
             .Include(o => o.Address)
             .Include(o => o.PaymentMethod)
             .SingleOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync(bool trackChanges)
        {
            return await _dbContext.Orders
                   .AsNoTracking() // Không theo dõi thay đổi của thực thể
                   .Where(x => !x.OrderStatus)
                   .Include(o => o.CartItems) // Nếu cần, bao gồm các thuộc tính liên quan
                   .ThenInclude(ci => ci.Product) // Nếu cần, bao gồm các thuộc tính liên quan của CartItems
                   .ToListAsync();
        }

        public async Task<Order> GetOrderPaymentByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders
                     .Include(o => o.CartItems)
                     .Include(o => o.Address)
                     .Include(o => o.PaymentMethodId)
                     .FirstOrDefaultAsync(o => o.Id == orderId);

        }

        public async Task DeleteOrderCheckoutAsync(Guid orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                // Xóa đơn hàng
                _dbContext.Orders.Remove(order);

                await SaveAsync();
            }
        }

        public async Task AddOrderItemsAsync(IEnumerable<OrderItem> orderItems)
        {
            await _dbContext.OrderItems.AddRangeAsync(orderItems);
        }

        public async Task<Order> SearchOrdersByCodeAsync(string orderCode, bool trackChanges)
        {
            return await FindByCondition(x => x.OrderCode.Contains(orderCode), trackChanges)
                  .FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
