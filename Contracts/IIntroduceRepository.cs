using Entities.Models;

namespace Contracts
{
    public interface IIntroduceRepository
    {
        Task<Introduce> CreateIntroduceAsync(Introduce introduce);
        Task<IEnumerable<Introduce>> GetAllIntroduceAsync(bool trackChanges);
        Task<Introduce> GetIntroduceByIdAsync(Guid introduceId, bool trackChanges);
        Task<Introduce> GetIntroduceByNameAsync(string name);
        void UpdateIntroduce(Introduce introduce);
        void DeleteIntroduce(Introduce introduce);
    }
}
