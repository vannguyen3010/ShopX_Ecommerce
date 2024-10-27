﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProductRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            foreach (var image in product.ProductImages)
            {
                await _dbContext.ProductImages.AddAsync(image);
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _dbContext.Products
                .Include(x => x.Category)   // Bao gồm thông tin danh mục sản phẩm nếu cần
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id, bool trackChanges)
        {
            if (trackChanges)
            {
                return await _dbContext.Products
                    .Include(p => p.ProductImages)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            else
            {
                return await _dbContext.Products
                    .AsNoTracking()
                    .Include(p => p.ProductImages)
                    .Include(p => p.Category) 
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
        }
        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductIsHotAsync()
        {
            //return await _dbContext.Products.Where(x => x.IsHot == true).ToListAsync();
            return await _dbContext.Products
                .Where(x => x.IsHot == true)
                .Include(x => x.ProductImages)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Product> Products, int Total)> GetAllProductPaginationAsync(int pageNumber, int pageSize)
        {
            var productsQuery = _dbContext.Products
                            .Include(x => x.ProductImages)
                            .AsQueryable();

            // Đếm tổng số lượng sản phẩm
            int totalCount = await productsQuery.CountAsync();

            // Thực hiện phân trang
            var products = await productsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<(IEnumerable<Product> Products, int Total)> GetProductsByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize)
        {
            var productsQuery = _dbContext.Products
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.ProductImages)
                .AsQueryable();

            // Đếm tổng số lượng sản phẩm trong danh mục
            int totalCount = await productsQuery.CountAsync();

            // Thực hiện phân trang
            var products = await productsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string keyword, bool trackChanges)
        {
            var lowerCaseName = keyword.ToLower();

            // Sử dụng tìm kiếm không phân biệt chữ hoa và chữ thường, đồng thời tìm kiếm các chuỗi con
            return await _dbContext.Products
                .Where(x => x.Name.ToLower().Contains(lowerCaseName))
                .Include(p => p.ProductImages)
                .ToListAsync();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường

            return await _dbContext.Products
              .Where(x => x.Name.ToLower() == lowerName) // So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductDiscountAsync(bool trackChanges)
        {
            return await _dbContext.Products
                .Where(x => x.Discount > 0)
                .Include(x => x.ProductImages)
                .ToListAsync();
        }

        //public async Task UpdateProductAsync(Product product, byte[] rowVersion)
        //{
        //    _dbContext.Entry(product).OriginalValues["RowVersion"] = rowVersion;
        //    _dbContext.Products.Update(product);
        //    await SaveAsync();
        //}
        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await SaveAsync();
        }
        public async Task UpdateProductOrderItemAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await SaveAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductOutByStockStatusAsync(int stockQuantity, bool trackChanges)
        {
            return await FindByCondition(x => x.StockQuantity == stockQuantity, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByStockStatus(int stockQuantity, bool trackChanges)
        {
            return await FindByCondition(x => x.StockQuantity == stockQuantity, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductOnByStockStatusAsync(int stockQuantity, bool trackChanges)
        {
            return await FindByCondition(x => x.StockQuantity > stockQuantity, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllNewProductsAsync(DateTime startDate, bool trackChanges)
        {
            return await _dbContext.Products
                        .Where(x => x.CreatedAt >= startDate)
                        .OrderByDescending(x => x.CreatedAt)// Sắp xếp theo ngày tạo mới nhất trước
                        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(Guid productId, Guid categoryId, bool trackChanges)
        {
            return await _dbContext.Products
                    .Where(x => x.CategoryId == categoryId && x.Id != productId)
                    .ToListAsync();
        }

        public async Task<(IEnumerable<Product>, int totalCount)> GetListProducAsync(int pageNumber, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            var query = _dbContext.Products.AsQueryable();

            //type = 3 : nổi bật, 4: giảm giá
            if (type.HasValue)
            {
                if(type == 3)
                {
                    query = query.Where(x => x.IsHot == true);
                }
                else if(type == 4)
                {
                    query = query.Where(x => x.Discount > 0);
                }
            }

            //Lọc theo giá tối thiểu
            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice.Value);
            }

            //Lọc theo gía tối đa
            if(maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= maxPrice.Value);
            }

            // Lọc theo danh mục sản phẩm (Guid)
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value).Include(x => x.ProductImages);
            }

            //Lọc theo keyword nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerCaseName = keyword.ToLower();

                query = query.Where(x => x.Name.ToLower().Contains(lowerCaseName));
            }

            var totalCount = await query.CountAsync();

            // Phân trang
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<IEnumerable<Product>> GetBestSellingProductsAsync(int bestSeller, bool trackChanges)
        {
            return await FindByCondition(x => x.BestSeller >= bestSeller, trackChanges).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

      
    }
}
