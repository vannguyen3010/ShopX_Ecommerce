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
        IAddressRepository Address { get; }
        IProvinceRepository Province { get; }
        IDistrictRepository District { get; }
        IWardRepository Ward { get; }
        IProfileRepository Profile { get; }
        ICartRepository Cart { get; }
        ICategoryIntroduceRepository CategoryIntroduce { get; }


        void SaveAsync();
    }
}
