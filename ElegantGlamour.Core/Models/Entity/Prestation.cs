
namespace ElegantGlamour.Core.Models
{
    public class Prestation : BaseEntity, IPrestation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int PrestationCategoryId { get; set; }
        public PrestationCategory PrestationCategory { get; set; }
        public float Price { get; set; }
    }
}