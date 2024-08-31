using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shared.DTO.Banner
{
    public class BannerDto
    {
        public string title { get; set; }
        public string descShort { get; set; }

        [Required, DisplayName("Product Image")]
        public string? Base64Img { get; set; }
        public int? status { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
