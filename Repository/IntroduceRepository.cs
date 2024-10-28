﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Repository
{
    public class IntroduceRepository : RepositoryBase<Introduce>, IIntroduceRepository
    {
        private readonly RepositoryContext _dbContext;

        public IntroduceRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Introduce> CreateIntroduceAsync(Introduce introduce)
        {
            await _dbContext.Introduces.AddAsync(introduce);
            await _dbContext.SaveChangesAsync();
            return introduce;
        }

        public async Task<(IEnumerable<Introduce> Introducdes, int Total)> GetAllIntroducePaginationAsync(int pageNumber, int pageSize, string? keyword = null)
        {
            var introducesQuery = _dbContext.Introduces.AsQueryable();

            //Lọc theo keyword nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerCaseName = keyword.ToLower();

                introducesQuery = introducesQuery.Where(x => x.Name.ToLower().Contains(lowerCaseName));
            }

            // Đếm tổng số lượng sản phẩm
            int totalCount = await introducesQuery.CountAsync();

            // Thực hiện phân trang
            var introduces = await introducesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (introduces, totalCount);
        }

        public async Task<Introduce> GetIntroduceByIdAsync(Guid introduceId, bool trackChanges)
        {
            //return await FindByCondition(introduce => introduce.id.Equals(introduceId), trackChanges).FirstOrDefaultAsync();
            if (trackChanges)
            {
                return await _dbContext.Introduces
                    .Include(x => x.CategoryIntroduce)
                    .FirstOrDefaultAsync(x => x.Id == introduceId);
            }
            else
            {
                return await _dbContext.Introduces
                 .AsNoTracking()
                .Include(x => x.CategoryIntroduce)
                .FirstOrDefaultAsync(x => x.Id == introduceId);
            }
        }

        public void UpdateIntroduce(Introduce introduce)
        {
            Update(introduce);
        }

        public void DeleteIntroduce(Introduce introduce)
        {
            Delete(introduce);
        }

        public async Task<Introduce> GetIntroduceByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường

            return await _dbContext.Introduces
              .Where(x => x.Name.ToLower() == lowerName) // So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
              .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Introduce>> GetAllIntroduceIsHotAsync()
        {
            return await _dbContext.Introduces.Where(x => x.IsHot).ToListAsync();
        }

        public async Task<IEnumerable<Introduce>> SearchIntroducesByNameAsync(string keyword, bool trackChanges)
        {
            var lowerCaseName = keyword.ToLower();

            // Sử dụng tìm kiếm không phân biệt chữ hoa và chữ thường, đồng thời tìm kiếm các chuỗi con
            return await _dbContext.Introduces
                .Where(x => x.Name.ToLower().Contains(lowerCaseName))
                .ToListAsync();
        }

        public async Task<(IEnumerable<Introduce> Introduces, int Total)> GetAllProductsByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize)
        {
            var introducesQuery = _dbContext.Introduces
              .Where(x => x.CategoryId == categoryId)
              .AsQueryable();

            // Đếm tổng số lượng sản phẩm trong danh mục
            int totalCount = await introducesQuery.CountAsync();

            // Thực hiện phân trang
            var products = await introducesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<(IEnumerable<Introduce> Introduces, int Total)> GetListIntroduceAsync(int pageNumber, int pageSize, Guid? categoryId = null, string? keyword = null, int? type = null)
        {
            var introducesQuery = _dbContext.Introduces.AsQueryable();

            if(type.HasValue)
            {
                if (type == 3)
                {
                    introducesQuery = introducesQuery.Where(x => x.IsHot == true);
                }
            }

            //Lọc bài viết theo danh mục
            if (categoryId.HasValue)
            {
                introducesQuery = introducesQuery.Where(x => x.CategoryId == categoryId.Value);
            }

            //Lọc theo keyword nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                string lowerCaseName = keyword.ToLower();

                introducesQuery = introducesQuery.Where(x => x.Name.ToLower().Contains(lowerCaseName));
            }

            int totalCount = await introducesQuery.CountAsync();

            //Phân trang
            var introduces = await introducesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (introduces, totalCount);
        }

        public async Task<IEnumerable<Introduce>> GetRelatedIntroducesAsync(Guid introduceId, Guid categoryId, bool trackChanges)
        {
            return await _dbContext.Introduces
                  .Where(x => x.CategoryId == categoryId && x.Id != introduceId)
                  .ToListAsync();
        }
    }
}
