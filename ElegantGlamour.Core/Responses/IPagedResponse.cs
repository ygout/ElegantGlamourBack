namespace ElegantGlamour.Core.Responses
{
    public interface IPagedResponse<T> : IListResponse<T>
    {
        int ItemsCount { get; set; }
        double PageCount { get; }
    }
}