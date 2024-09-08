using Entities.Models.Address;

namespace Contracts
{
    public interface IWardRepository
    {
        Task<Ward> GetWardByCodeAsync(string code);
    }
}
