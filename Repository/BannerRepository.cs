using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repository
{
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
    {
        private readonly RepositoryContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BannerRepository(RepositoryContext dbContext, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Banner> CreateBanner(Banner banner)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Img_Repository/Banner", $"{banner.FileName}{banner.FileExtension}");

            //Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await banner.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Img_Repository/Banner/{banner.FileName}{banner.FileExtension}";

            banner.FilePath = urlFilePath;

            //Add Image to the Images table
            await _dbContext.Banners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();

            return banner;
        }
        public async Task<IEnumerable<Banner>> GetAllBannersAsync(BannerPosition? position = null)
        {
            if(position.HasValue)
            {
                return await _dbContext.Banners.Where(x => x.Position == position).ToListAsync();
            }
            else
            {
                return await _dbContext.Banners.ToListAsync();
            }
        }
        public async Task<Banner> GetBannerByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }
        public async Task UpdateBanner(Banner banner)
        {
            _dbContext.Banners.Update(banner);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBanner(Guid id)
        {
            var banner = await _dbContext.Banners.FindAsync(id);
            if(banner != null)
            {
                _dbContext.Banners.Remove(banner);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
