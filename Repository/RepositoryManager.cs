using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repoContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBannerRepository _banner;
        private ICateProductsRepository _categoryProduct;
        private IContactRepository _contact;
        private IProductRepository _product;
        private ICommentProductRepository _commentProduct;

        public RepositoryManager(RepositoryContext repositoryContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _repoContext = repositoryContext;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public IBannerRepository Banner
        {
            get
            {
                if (_banner == null)
                {
                    _banner = new BannerRepository(_repoContext, _httpContextAccessor, _webHostEnvironment);
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

        public void SaveAsync() => _repoContext.SaveChanges();
    }
}
