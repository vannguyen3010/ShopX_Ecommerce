namespace Shared.DTO.CommentProduct
{
    public class CommentProductDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Content { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
