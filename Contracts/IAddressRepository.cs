using Entities.Models;
using Entities.Models.Address;

namespace Contracts
{
    public interface IAddressRepository
    {
        Task CreateAddressAsync(Address address);
        Task<Address> GetAddressByNameAsync(string name);
        Task UpdateAddressAsync(Address address);
        Task<Address> GetAddressByIdAsync(Guid addressId, bool trackChanges);
        Task<IEnumerable<Address>> GetAllAddressAsync(bool trackChanges);
        Task DeleteAddress(Address address);
        Task SaveAsync();
    }
}
