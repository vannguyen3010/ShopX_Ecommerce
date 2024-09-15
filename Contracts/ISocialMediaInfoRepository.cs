using Entities.Models;

namespace Contracts
{
    public interface ISocialMediaInfoRepository
    {
        Task<SocialMediaInfo> GetSocialMediaInfoAsync(Guid id, bool trackChanges);
        Task CreateSocialMediaInfoAsync(SocialMediaInfo socialMediaInfo);
        Task UpdateSocialMediaInfoAsync(SocialMediaInfo socialMediaInfo);
    }
}
