using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Models
{
    public class PortfolioImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public Category category { get; set; }
    }
}