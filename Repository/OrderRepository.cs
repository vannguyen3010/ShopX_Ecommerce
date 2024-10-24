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
             .SingleOrDefaultAsync(o => o.Id == orderId);
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int type, bool trackChanges)
        {
            IQueryable<Order> ordersQuery = _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.CartItems)
                .ThenInclude(x => x.Product);

            // Lọc theo type
            if(type == 1)
            {
                ordersQuery = ordersQuery.Where(x => x.OrderStatus);
            }
            else if(type == 2)
            {
                ordersQuery = ordersQuery.Where(x => !x.OrderStatus);
            }

            return await ordersQuery.ToListAsync();
        }

        public async Task<Order> GetOrderPaymentByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders
                     .Include(o => o.CartItems)
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

        public async Task<(IEnumerable<Order> Orders, int Total)> GetAllOrdersByUserIdAsync(string userId, string keyword, int pageNumber, int pageSize)
        {
            // Tạo một query lọc theo UserId
            var query = _dbContext.Orders.Where(order => order.UserId == userId);

            // Nếu keyword không null, thêm điều kiện lọc theo mã đơn hàng (OrderCode)
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(order => order.OrderCode.Contains(keyword));
            }

            // Đếm tổng số đơn hàng trước khi phân trang
            var totalOrders = await query.CountAsync();

            // Thực hiện phân trang
            var orders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalOrders);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
