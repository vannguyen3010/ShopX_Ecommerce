using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository
    {
        void CreateCategoryAsync(Category category);
    }
}
