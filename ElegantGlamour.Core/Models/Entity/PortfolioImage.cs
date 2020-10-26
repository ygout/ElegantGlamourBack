using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Models
{
    public class PortfolioImage : BaseEntity
    {
        public string Path { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public PrestationCategory PrestationCategory { get; set; }
    }
}