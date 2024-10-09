using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CategoryIntroduceRepository : RepositoryBase<CategoryIntroduce>, ICategoryIntroduceRepository
    {
        private readonly RepositoryContext _dbContext;

        public CategoryIntroduceRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryIntroduce> CreateCategoryIntroduceAsync(CategoryIntroduce categoryIntroduce, bool trackChanges)
        {
            await _dbContext.CategoryIntroduces.AddAsync(categoryIntroduce);
            await SaveAsync();
            return categoryIntroduce;
        }


        public async Task<IEnumerable<CategoryIntroduce>> GetAllCategoryIntroduceAsync(bool trackChanges)
        {
            return await _dbContext.CategoryIntroduces
                    .Include(x => x.Introduces)
                    .OrderBy(x => x.Id)
                    .ToListAsync();

        }

        public async Task<CategoryIntroduce> GetCategoryIntroduceByIdAsync(Guid categoryId, bool trackChanges)
        {
            return await FindByCondition(category => category.Id.Equals(categoryId), trackChanges)
           .FirstOrDefaultAsync();
        }

        public void DeleteCategory(CategoryIntroduce categoryIntroduce)
        {
            Delete(categoryIntroduce);
        }
        public void UpdateCategory(CategoryIntroduce categoryIntroduce)
        {
            Update(categoryIntroduce);
        }

        public async Task<CategoryIntroduce> GetCategoryIntroduceByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường

            return await _dbContext.CategoryIntroduces
              .Where(x => x.Name.ToLower() == lowerName) // So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
              .FirstOrDefaultAsync();
        }
        public async Task<bool> HasIntroducesInCategoryAsync(Guid categoryId)
        {
            return await _dbContext.Introduces.AnyAsync(x => x.CategoryId == categoryId);
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    
    }
}
