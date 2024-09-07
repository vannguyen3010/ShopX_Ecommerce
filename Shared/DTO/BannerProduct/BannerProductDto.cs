using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.BannerProduct
{
    public class BannerProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FilePath { get; set; }
    }
}
