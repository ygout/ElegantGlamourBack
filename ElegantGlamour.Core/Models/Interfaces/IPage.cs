namespace ElegantGlamour.Core.Models
{
    public interface IPage
    {
        int Id { get; set; }
        string Title { get; set; }
        string Content { get; set; }
    }
}