using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order); // Tạo đơn hàng
        Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges);
        Task<Order> GetOrderByUserIdAsync(string userId);
        Task<Order> GetOrderDetailsForPaymentAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        void DeleteOrderAsync(Order order);
        Task SaveAsync();
    }
}
