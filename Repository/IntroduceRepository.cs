using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class IntroduceRepository : RepositoryBase<Introduce>, IIntroduceRepository
    {
        private readonly RepositoryContext _dbContext;

        public IntroduceRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Introduce> CreateIntroduceAsync(Introduce introduce)
        {
            await _dbContext.Introduces.AddAsync(introduce);
            await _dbContext.SaveChangesAsync();
            return introduce;
        }

        public async Task<IEnumerable<Introduce>> GetAllIntroduceAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.Titlte).ToListAsync();
        }

        public async Task<Introduce> GetIntroduceByIdAsync(Guid introduceId, bool trackChanges)
        {
            return await FindByCondition(introduce => introduce.id.Equals(introduceId), trackChanges).FirstOrDefaultAsync();
        }

        public void UpdateIntroduce(Introduce introduce)
        {
            Update(introduce);
        }

        public void DeleteIntroduce(Introduce introduce)
        {
            Delete(introduce);
        }

    }
}
