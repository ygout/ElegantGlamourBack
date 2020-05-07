namespace ElegantGlamour.Core.Models
{
    public class UpdatePrestationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public int CategoryId { get; set; }
    }
}