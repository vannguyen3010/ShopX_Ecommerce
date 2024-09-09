using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProfileRepository : RepositoryBase<Image>, IProfileRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProfileRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateImageProfileAsync(Image image)
        {
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Image> GetImageByIdAsync(Guid id)
        {
            return await _dbContext.Images.FindAsync(id);
        }

        public async Task<ProfileUser> GetProfileByUserIdAsync(string userId)
        {
            return await _dbContext.ProfileUsers
                .Include(x => x.User)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
