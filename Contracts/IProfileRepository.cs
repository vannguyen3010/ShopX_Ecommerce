using Entities.Identity;
using Entities.Models;

namespace Contracts
{
    public interface IProfileRepository
    {
        Task CreateImageProfileAsync(Image image);
        Task<Image> GetImageByIdAsync(Guid id);
        Task<ProfileUser> GetProfileByUserIdAsync(string userId);
        //Task<ProfileUser> GetUserByIdAsync(Guid id, bool trackChanges);
    }
}
