namespace Contracts
{
    public interface IRepositoryManager
    {
        IBannerRepository Banner { get; }

        ICategoryRepository Category { get; }
        IContactRepository Contact { get; }

        void SaveAsync();
    }
}
