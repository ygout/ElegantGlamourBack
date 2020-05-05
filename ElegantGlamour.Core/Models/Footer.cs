namespace ElegantGlamour.Core.Models
{
    public class FooterGrid {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
    }
    public class Footer
    {
        public int Id { get; set; }
        public FooterGrid[] Grids { get; set; } = new FooterGrid[3];
        
    }
}