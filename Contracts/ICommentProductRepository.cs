using Entities.Identity;
using Entities.Models;

namespace Contracts
{
    public interface ICommentProductRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task CreateCommentAsync(CommentProduct comment);
        Task<IEnumerable<CommentProduct>> GetRecentCommentsByUserAsync(string userId, int limit);
        Task<CommentProduct> GetCommentByIdAsync(Guid commentId, bool trackChanges);
        Task<IEnumerable<CommentProduct>> GetAllCommentsByProductIdAsync(Guid productId, bool trackChanges);
        Task<IEnumerable<CommentProduct>> GetAllCommentsAsync(bool trackChanges);
    }
}
