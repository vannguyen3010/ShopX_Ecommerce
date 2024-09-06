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

        public async void DeleteComment(CommentProduct comment)
        {
            _dbContext.CommentProducts.Remove(comment);
        }

        public async Task<CommentProduct> GetLastCommentByUserAndProductAsync(string userId, Guid productId)
        {
            return await _dbContext.CommentProducts
           .Where(x => x.UserId == userId && x.ProductId == productId)
           .OrderByDescending(x => x.CreatedAt)
           .FirstOrDefaultAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
