using Entities.Models;

namespace Contracts
{
    public interface ICheckoutRepository
    {
        Task<IEnumerable<Checkout>> GetAllCheckoutAsync();
        Task<IEnumerable<Checkout>> GetAllPendingCheckoutOrdersAsync(bool trackChanges);
        Task<IEnumerable<Checkout>> GetAllConfirmCheckoutOrdersAsync(bool trackChanges);
        Task<Checkout> GetCheckoutByIdAsync(Guid checkoutId);
        Task CreateCheckoutAsync(Checkout checkout);
        Task UpdateCheckoutAsync(Checkout checkout);
        Task DeleteCheckoutAsync(Checkout checkout);
        Task SaveAsync();
    }
}
