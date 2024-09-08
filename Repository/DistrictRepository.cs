using Contracts;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        private readonly RepositoryContext _dbContext;

        public DistrictRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<District> GetDistrictByCodeAsync(string code)
        {
            return await _dbContext.Districts.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<IEnumerable<Ward>> GetWardsByDistrictCodeAsync(string districtCode)
        {
            return await _dbContext.Wards.Where(w => w.DistrictCode == districtCode).ToListAsync();
        }
    }
}
