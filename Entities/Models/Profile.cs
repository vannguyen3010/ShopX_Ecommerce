using Entities.Identity;

namespace Entities.Models
{
    public class ProfileUser
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? ImageId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public User User { get; set; } 
        public Image Image { get; set; }
    }
}
