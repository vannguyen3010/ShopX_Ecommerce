using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;

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

        public void SaveAsync() => _repoContext.SaveChanges();
    }
}
