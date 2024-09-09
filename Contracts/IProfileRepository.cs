using Entities.Identity;
using Entities.Models;

namespace Contracts
{
    public interface IProfileRepository
    {
        //Task<User> GetUserByIdAsync(string userId);
        Task CreateImageProfileAsync(Image image);
        Task<Image> GetImageByIdAsync(Guid id, bool trackChanges);
        void UpdateImageProfile(Image image);

    }
}
