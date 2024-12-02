using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repoContext;
        private IBannerRepository _banner;
        private ICateProductsRepository _categoryProduct;
        private IContactRepository _contact;
        private IProductRepository _product;
        private ICommentProductRepository _commentProduct;
        private IBannerProductRepository _bannerProduct;
        private IIntroduceRepository _introduce;
        private IProvinceRepository _province;
        private IDistrictRepository _district;
        private IWardRepository _ward;
        private IAddressRepository _address;
        private IProfileRepository _profile;
        private ICartRepository _cart;
        private ICategoryIntroduceRepository _categoryIntroduce;
        private IPaymentMethodRepository _paymentMethod;
        private IShippingCostRepository _shippingCost;
        private IOrderRepository _order;
        private ICheckoutRepository _checkout;
        private ISocialMediaInfoRepository _socialMediaInfo;
        private IAccountRepository _accountRepository;
        private IRoleRepository _roleRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IBannerRepository Banner
        {
            get
            {
                if (_banner == null)
                {
                    _banner = new BannerRepository(_repoContext);
                }

                return _banner;
            }
        }
        public IContactRepository Contact
        {
            get
            {
                if (_contact == null)
                {
                    _contact = new ContactRepository(_repoContext);
                }

                return _contact;
            }
        }

        public ICateProductsRepository CateProduct
        {
            get
            {
                if (_categoryProduct == null)
                {
                    _categoryProduct = new CateProductsRepository(_repoContext);
                }

                return _categoryProduct;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext);
                }

                return _product;
            }
        }

        public ICommentProductRepository CommentProduct
        {
            get
            {
                if (_commentProduct == null)
                {
                    _commentProduct = new CommentProductRepository(_repoContext);
                }

                return _commentProduct;
            }
        }

        public IBannerProductRepository BannerProduct
        {
            get
            {
                if (_bannerProduct == null)
                {
                    _bannerProduct = new BannerProductRepository(_repoContext);
                }

                return _bannerProduct;
            }
        }

        public IIntroduceRepository Introduce
        {
            get
            {
                if (_introduce == null)
                {
                    _introduce = new IntroduceRepository(_repoContext);
                }

                return _introduce;
            }
        }

        public IProvinceRepository Province
        {
            get
            {
                if (_province == null)
                {
                    _province = new ProvinceRepository(_repoContext);
                }

                return _province;
            }
        }

        public IDistrictRepository District
        {
            get
            {
                if (_district == null)
                {
                    _district = new DistrictRepository(_repoContext);
                }

                return _district;
            }
        }
        public IWardRepository Ward
        {
            get
            {
                if (_ward == null)
                {
                    _ward = new WardRepository(_repoContext);
                }

                return _ward;
            }
        }

        public IAddressRepository Address

        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_repoContext);
                }

                return _address;
            }
        }

        public IProfileRepository Profile
        {
            get
            {
                if (_profile == null)
                {
                    _profile = new ProfileRepository(_repoContext);
                }

                return _profile;
            }

        }

        public ICartRepository Cart
        {
            get
            {
                if (_cart == null)
                {
                    _cart = new CartRepository(_repoContext);
                }

                return _cart;
            }
        }

        public ICategoryIntroduceRepository CategoryIntroduce
        {
            get
            {
                if (_categoryIntroduce == null)
                {
                    _categoryIntroduce = new CategoryIntroduceRepository(_repoContext);
                }

                return _categoryIntroduce;
            }
        }

        public IPaymentMethodRepository PaymentMethod
        {
            get
            {
                if (_paymentMethod == null)
                {
                    _paymentMethod = new PaymentMethodRepository(_repoContext);
                }

                return _paymentMethod;
            }
        }

        public IShippingCostRepository ShippingCost
        {
            get
            {
                if (_shippingCost == null)
                {
                    _shippingCost = new ShippingCostRepository(_repoContext);
                }

                return _shippingCost;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_repoContext);
                }

                return _order;
            }
        }

        public ICheckoutRepository Checkout
        {
            get
            {
                if (_checkout == null)
                {
                    _checkout = new CheckoutRepository(_repoContext);
                }

                return _checkout;
            }
        }

        public ISocialMediaInfoRepository SocialMediaInfo
        {
            get
            {
                if (_socialMediaInfo == null)
                {
                    _socialMediaInfo = new SocialMediaInfoRepository(_repoContext);
                }

                return _socialMediaInfo;
            }
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repoContext);
                }

                return _accountRepository;
            }
        }

        public IRoleRepository RoleRepository => _roleRepository;

        public void SaveAsync() => _repoContext.SaveChanges();
    }
}
