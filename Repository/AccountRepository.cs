using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<RefreshToken>, IAccountRepository
    {
        private readonly RepositoryContext _dbContext;

        public AccountRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }

        public async Task<RefreshToken> GetByUserIdAsync(string userId)
        {
            return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId && !rt.IsRevoked);
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _dbContext.RefreshTokens.Update(refreshToken);
            await _dbContext.SaveChangesAsync();
        }
    }
}
