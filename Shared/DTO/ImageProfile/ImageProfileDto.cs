namespace Shared.DTO.ImageProfile
{
    public class ImageProfileDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
