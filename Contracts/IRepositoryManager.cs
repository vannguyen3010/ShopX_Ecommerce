namespace Contracts
{
    public interface IRepositoryManager
    {
        IBannerRepository Banner { get; }

        ICateProductsRepository CateProduct { get; }
        IContactRepository Contact { get; }

        void SaveAsync();
    }
}
