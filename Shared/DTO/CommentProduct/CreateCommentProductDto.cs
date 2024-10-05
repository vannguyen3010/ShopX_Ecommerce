using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.CommentProduct
{
    public class CreateCommentProductDto
    {
        [Required]
        public string UserId { get; set; }
        //[Required]
        //public string Token { get; set; }
        [Required]
        public Guid ProductId { get; set; } // ID sản phẩm được comment
        [Required]
        public string Content { get; set; } // Nội dung comment
    }
   
}
