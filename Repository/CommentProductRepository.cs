using Contracts;
using Entities.Identity;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CommentProductRepository : RepositoryBase<CommentProduct>, ICommentProductRepository
    {
        private readonly RepositoryContext _dbContext;

        public CommentProductRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCommentAsync(CommentProduct comment)
        {
            await _dbContext.CommentProducts.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<IEnumerable<CommentProduct>> GetRecentCommentsByUserAsync(string userId, int limit)
        {
            return await _dbContext.CommentProducts
               .Where(c => c.UserId == userId)
               .OrderByDescending(c => c.CreatedAt)
               .Take(limit)
               .ToListAsync();
        }

        public async Task<CommentProduct> GetCommentByIdAsync(Guid commentId, bool trackChanges)
        {
            return await (trackChanges
                ? _dbContext.CommentProducts
                : _dbContext.CommentProducts.AsNoTracking())
                .SingleOrDefaultAsync(x => x.Id == commentId);
        }

        public async Task<IEnumerable<CommentProduct>> GetAllCommentsByProductIdAsync(Guid productId, bool trackChanges)
        {
            return await (trackChanges
                ? _dbContext.CommentProducts.Include(x => x.User)
                : _dbContext.CommentProducts.AsNoTracking().Include(x => x.User))
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CommentProduct>> GetAllCommentsAsync(bool trackChanges)
        {
            return await (trackChanges
                ? _dbContext.CommentProducts.Include(x => x.User).Include(x => x.Product)
                : _dbContext.CommentProducts.AsNoTracking().Include(x => x.User).Include(x => x.Product))
                .ToListAsync();
        }
    }
}
