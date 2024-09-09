using Contracts;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        private readonly RepositoryContext _dbContext;

        public AddressRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAddressAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId, bool trackChanges)
        {
            return await FindByCondition(address => address.Id.Equals(addressId), trackChanges)
                .FirstOrDefaultAsync();
        }

        public async Task<Address> GetAddressByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường
            return await _dbContext.Addresses
                .Where(x => x.StreetAddress.ToLower() == lowerName)// So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddressAsync(bool trackChanges)
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _dbContext.Addresses.Update(address);
            await _dbContext.SaveChangesAsync();
        }
    }
}
