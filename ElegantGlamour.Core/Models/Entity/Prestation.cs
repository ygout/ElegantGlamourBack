using ElegantGlamour.Core.Models.Entity;

namespace ElegantGlamour.Core.Models
{
    public class Prestation : BaseEntity, IPrestation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public float Price { get; set; }
    }
}