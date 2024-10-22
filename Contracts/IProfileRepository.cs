using Entities.Identity;
using Entities.Models;
using Entities.Models.Address;

namespace Contracts
{
    public interface IProfileRepository
    {
        Task CreateProfileUserAsync(ProfileUser profile);
        Task<ProfileUser> GetProfileByUserIdAsync(string userId, bool trackChanges);
        Task<ProfileUser> GetProfileByIdAsync(Guid id, bool trackChanges);
        Task DeleteProfileAsync(ProfileUser profileUser);
        Task UpdateProfileUserAsync(ProfileUser profileUser);

    }
}
