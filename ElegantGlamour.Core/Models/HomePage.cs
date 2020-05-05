namespace ElegantGlamour.Core.Models
{
    public class HomePage : IPage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerDescription { get; set; }
        public string[] ImagesContentPath { get; set; } = new string[2];

    }
}