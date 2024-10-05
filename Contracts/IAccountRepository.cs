using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetByUserIdAsync(string userId);
    }
}
