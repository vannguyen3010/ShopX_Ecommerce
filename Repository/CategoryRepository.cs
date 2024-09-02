using Contracts;
using Entities.Models;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateCategoryAsync(Category category)
        {
            Create(category);
        }
    }
}
