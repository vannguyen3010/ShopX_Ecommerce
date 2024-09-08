using Entities.Models;
using Entities.Models.Address;

namespace Contracts
{
    public interface IAddressRepository
    {
        Task CreateAddressAsync(Location location);
        Task<Location> GetAddressByNameAsync(string name);
    }
}
