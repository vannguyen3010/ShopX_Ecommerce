using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTO.ImageProfile
{
    public class CreateProfileUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string TikTokLink { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile LogoUrl { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
