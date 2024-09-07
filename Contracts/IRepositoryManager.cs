namespace Contracts
{
    public interface IRepositoryManager
    {
        IBannerRepository Banner { get; }

        ICateProductsRepository CateProduct { get; }
        IContactRepository Contact { get; }
        IProductRepository Product { get; }
        ICommentProductRepository CommentProduct { get; }
        IBannerProductRepository BannerProduct { get; }
        IIntroduceRepository Introduce { get; }


        void SaveAsync();
    }
}
