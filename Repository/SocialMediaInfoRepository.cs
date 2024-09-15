using Contracts;
using Entities.Models;

namespace Repository
{
    public class SocialMediaInfoRepository : RepositoryBase<SocialMediaInfo>, ISocialMediaInfoRepository
    {
        private readonly RepositoryContext _dbContext;

        public SocialMediaInfoRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<SocialMediaInfo> GetSocialMediaInfoAsync(Guid id, bool trackChanges)
        {
            return await _dbContext.SocialMediaInfos.FindAsync(id);
        }

        public async Task CreateSocialMediaInfoAsync(SocialMediaInfo socialMediaInfo)
        {
            await _dbContext.SocialMediaInfos.AddAsync(socialMediaInfo);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateSocialMediaInfoAsync(SocialMediaInfo socialMediaInfo)
        {
            _dbContext.SocialMediaInfos.Update(socialMediaInfo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
