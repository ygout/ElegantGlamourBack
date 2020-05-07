namespace ElegantGlamour.Core.Models
{
    public enum LINK_TYPE  {
        INSTAGRAM,
        FACEBOOK,
        LINKEDIN,
        PRINTEREST
    }
    public class SocialLink
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LinkPath { get; set; }
        public LINK_TYPE Type { get; set; }
    }
}