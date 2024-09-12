using Entities.Models;

namespace Contracts
{
    public interface IPaymentMethodRepository
    {
        Task CreatePaymentMethod(PaymentMethod paymentMethod);
        Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(Guid Id, bool trackChanges);
        void UpdatePaymentMethod(PaymentMethod paymentMethod);
        void DeletePaymentMethod(PaymentMethod paymentMethod);
        Task<bool> SaveAsync();
    }
}
