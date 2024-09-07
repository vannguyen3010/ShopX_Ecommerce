using Entities.Models;

namespace Contracts
{
    public interface IIntroduceRepository
    {
        Task<Introduce> CreateIntroduceAsync(Introduce introduce);
        //Task<IEnumerable<Introduce>> GetAllIntroduceAsync(bool trackChanges);
        Task<(IEnumerable<Introduce> Introducdes, int Total)> GetAllIntroducePaginationAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Introduce>> GetAllIntroduceIsHotAsync();
        Task<Introduce> GetIntroduceByIdAsync(Guid introduceId, bool trackChanges);
        Task<Introduce> GetIntroduceByNameAsync(string name);
        Task<IEnumerable<Introduce>> SearchIntroducesByNameAsync(string keyword, bool trackChanges);
        void UpdateIntroduce(Introduce introduce);
        void DeleteIntroduce(Introduce introduce);
    }
}
