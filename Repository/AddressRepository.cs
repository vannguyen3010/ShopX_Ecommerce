using Contracts;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AddressRepository : RepositoryBase<Location>, IAddressRepository
    {
        private readonly RepositoryContext _dbContext;

        public AddressRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAddressAsync(Location address)
        {
            await _dbContext.Locations.AddAsync(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Location> GetAddressByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường
            return await _dbContext.Locations
                .Where(x => x.StreetAddress.ToLower() == lowerName)// So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
                .FirstOrDefaultAsync();
        }
    }
}
