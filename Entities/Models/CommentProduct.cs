using Entities.Identity;

namespace Entities.Models
{
    public class CommentProduct
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
