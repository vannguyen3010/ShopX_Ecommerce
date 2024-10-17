using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order); // Tạo đơn hàng
        Task<Order> GetOrderByIdAsync(Guid orderId, bool trackChanges);
        Task<Order> GetOrderByUserIdAsync(string userId);
        Task<Order> GetOrderDetailsForPaymentAsync(Guid orderId);
        Task<IEnumerable<Order>> GetPendingOrdersAsync(bool trackChanges);
        Task<Order> GetOrderPaymentByIdAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        void DeleteOrderAsync(Order order);
        Task DeleteOrderCheckoutAsync(Guid orderId);
        Task AddOrderItemsAsync(IEnumerable<OrderItem> orderItems);
        Task SaveAsync();
    }
}
