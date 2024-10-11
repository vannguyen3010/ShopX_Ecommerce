using Entities.Models;

namespace Contracts
{
    public interface IIntroduceRepository
    {
        Task<Introduce> CreateIntroduceAsync(Introduce introduce);
        Task<(IEnumerable<Introduce> Introduces, int Total)> GetListIntroduceAsync(int pageNumber, int pageSize, Guid? categoryId = null, string ? keyword = null, int? type = null);
        Task<(IEnumerable<Introduce> Introducdes, int Total)> GetAllIntroducePaginationAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Introduce>> GetAllIntroduceIsHotAsync();
        Task<Introduce> GetIntroduceByIdAsync(Guid introduceId, bool trackChanges);
        Task<Introduce> GetIntroduceByNameAsync(string name);
        Task<IEnumerable<Introduce>> SearchIntroducesByNameAsync(string keyword, bool trackChanges);
        Task<(IEnumerable<Introduce> Introduces, int Total)> GetAllProductsByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize);
        Task<IEnumerable<Introduce>> GetRelatedIntroducesAsync(Guid introduceId, Guid categoryId, bool trackChanges);
        void UpdateIntroduce(Introduce introduce);
        void DeleteIntroduce(Introduce introduce);
    }
}
