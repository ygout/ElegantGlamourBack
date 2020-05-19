using System.Collections.Generic;

namespace ElegantGlamour.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Prestation> Prestations { get; set; }
    }
}