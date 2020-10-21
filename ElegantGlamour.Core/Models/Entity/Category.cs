using System.Collections.Generic;
using ElegantGlamour.Core.Models.Entity;

namespace ElegantGlamour.Core.Models
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Prestation> Prestations { get; set; }
    }
}