using System.Collections.Generic;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Models
{
    public class GiftPrestation : BaseEntity, IPrestation
    {
        public string Title { get; set; }
        public List<Prestation> Prestations { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}