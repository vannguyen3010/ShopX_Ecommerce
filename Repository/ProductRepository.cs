using Contracts;
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
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductIsHot()
        {
            return await _dbContext.Products.Where(x => x.IsHot == true).ToListAsync();
        }

        public async Task<(IEnumerable<Product> Products, int Total)> GetAllProductPagination(int pageNumber, int pageSize)
        {
            var productsQuery = _dbContext.Products.AsQueryable();

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
                .ToListAsync();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường

            return await _dbContext.Products
              .Where(x => x.Name.ToLower() == lowerName) // So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
              .FirstOrDefaultAsync();
        }
    }
}
