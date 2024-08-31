using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Banner
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string descShort { get; set; }

        [Required, DisplayName("Product Image")]
        public string? Base64Img { get; set; }
        public int? status { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
