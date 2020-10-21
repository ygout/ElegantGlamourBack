using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Models.Entity;

namespace ElegantGlamour.Core.Models
{
    public class PortfolioImage : BaseEntity
    {
        public string Path { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public Category category { get; set; }
    }
}