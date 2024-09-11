using Entities.Models;

namespace Contracts
{
    public interface ICategoryIntroduceRepository
    {
        Task<CategoryIntroduce> CreateCategoryIntroduceAsync(CategoryIntroduce categoryIntroduce, bool trackChanges);
        Task<IEnumerable<CategoryIntroduce>> GetAllCategoryIntroduceAsync(bool trackChanges);
        Task<CategoryIntroduce> GetCategoryIntroduceByIdAsync(Guid categoryId, bool trackChanges);
        Task<CategoryIntroduce> GetCategoryIntroduceByNameAsync(string name);
        Task<bool> HasIntroducesInCategoryAsync(Guid categoryId);
        void UpdateCategory(CategoryIntroduce categoryIntroduce);
        void DeleteCategory(CategoryIntroduce categoryIntroduce);
        Task SaveAsync();
    }
}
