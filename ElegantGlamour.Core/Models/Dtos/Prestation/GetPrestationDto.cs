
namespace ElegantGlamour.Core.Models
{
    public class GetPrestationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public Category Category { get; set; }
    }
}