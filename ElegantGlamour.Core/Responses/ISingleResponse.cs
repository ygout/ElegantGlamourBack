namespace ElegantGlamour.Core.Responses
{
    public interface ISingleResponse<T> : IResponse
    {
        T Data { get; set; }
    }
}