using Contracts;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WardRepository : RepositoryBase<Ward>, IWardRepository
    {
        private readonly RepositoryContext _dbContext;

        public WardRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ward> GetWardByCodeAsync(string code)
        {
            return await _dbContext.Wards.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
