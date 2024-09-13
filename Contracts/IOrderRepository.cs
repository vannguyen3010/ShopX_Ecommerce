using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order); // Tạo đơn hàng
        Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges);
        Task<Order> GetOrderByUserIdAsync(string userId);
        void DeleteOrderAsync(Order order);
        Task SaveAsync();
    }
}
