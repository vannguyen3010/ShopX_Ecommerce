using Contracts;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProvinceRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Province>> GetAllProvinceAsync(bool trackChanges)
        {
            return await _dbContext.Provinces.ToListAsync();
        }

        public async Task<IEnumerable<District>> GetDistrictsByProvinceCodeAsync(string provinceCode)
        {
            return await _dbContext.Districts.Where(x => x.ProvinceCode == provinceCode).ToListAsync();
        }

        public async Task<Province> GetProvinceByCodeAsync(string code)
        {
            return await _dbContext.Provinces.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
