namespace Shared.DTO.Banner
{
    public class BannerDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string filePath { get; set; }
        public BannerPosition Position { get; set; }
    }
}
