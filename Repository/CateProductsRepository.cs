using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Repository
{
    public class CateProductsRepository : RepositoryBase<CateProduct>, ICateProductsRepository
    {
        private readonly RepositoryContext _dbContext;

        public CateProductsRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CateProduct> CreateCategoryAsync(CateProduct cateProducs)
        {
            await _dbContext.CateProducts.AddAsync(cateProducs);
            await _dbContext.SaveChangesAsync();
            return cateProducs;
        }
    }
}
