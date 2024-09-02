namespace Contracts
{
    public interface IRepositoryManager
    {
        IBannerRepository Banner { get; }

        ICategoryRepository Category { get; }

        void SaveAsync();
    }
}
