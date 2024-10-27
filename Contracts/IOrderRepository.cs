using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order); // Tạo đơn hàng
        Task<Order> GetOrderByIdAsync(Guid orderId, bool trackChanges);
        Task<Order> GetOrderByUserIdAsync(string userId);
        Task<Order> GetOrderDetailsForPaymentAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync(int type, bool trackChanges);
        Task<Order> GetOrderPaymentByIdAsync(Guid orderId);
        Task UpdateOrderAsync(Order order);
        void DeleteOrderAsync(Order order);
        Task DeleteOrderCheckoutAsync(Guid orderId);
        Task AddOrderItemsAsync(IEnumerable<OrderItem> orderItems);
        Task<Order> SearchOrdersByCodeAsync(string orderCode, bool trackChanges);
        Task<(IEnumerable<Order> Orders, int Total)> GetAllOrdersByUserIdAsync(string userId, string keyword, int pageNumber, int pageSize);
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime fromDate, bool trackChanges);
        Task SaveAsync();
    }
}
