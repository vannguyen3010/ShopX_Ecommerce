using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        private readonly RepositoryContext _dbContext;

        public PaymentMethodRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            await _dbContext.PaymentMethods.AddAsync(paymentMethod);
        }


        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _dbContext.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(Guid Id, bool trackChanges)
        {
            //return await _dbContext.PaymentMethods.FindAsync(Id);
            if (trackChanges)
                return await _dbContext.PaymentMethods.FindAsync(Id);
            else
                return await _dbContext.PaymentMethods.AsNoTracking().FirstOrDefaultAsync(pm => pm.Id == Id);
        }

        public void UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            _dbContext.PaymentMethods.Update(paymentMethod);
        }

        public void DeletePaymentMethod(PaymentMethod paymentMethod)
        {
            _dbContext.PaymentMethods.Remove(paymentMethod);
        }
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
