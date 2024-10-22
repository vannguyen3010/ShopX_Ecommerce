using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProfileRepository : RepositoryBase<ProfileUser>, IProfileRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProfileRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProfileUserAsync(ProfileUser profile)
        {
            await _dbContext.ProfileUsers.AddAsync(profile);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<ProfileUser> GetProfileByIdAsync(Guid id, bool trackChanges)
        {
            return await _dbContext.ProfileUsers.FindAsync(id);
        }

        public async Task DeleteProfileAsync(ProfileUser profileUser)
        {
            Delete(profileUser);
        }

        public async Task<ProfileUser> GetProfileByUserIdAsync(string userId, bool trackChanges)
        {
            return await _dbContext.ProfileUsers.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task UpdateProfileUserAsync(ProfileUser profileUser)
        {
            _dbContext.ProfileUsers.Update(profileUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
