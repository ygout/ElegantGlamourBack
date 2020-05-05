namespace ElegantGlamour.Core.Models
{
    public interface IPrestation
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        float Price { get; set; }
        int Duration { get; set; }
    }
}