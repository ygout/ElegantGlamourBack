using ElegantGlamour.Core.Responses;

namespace ElegantGlamour.Api.Responses
{
    public class SingleResponse<T> : ISingleResponse<T>
    {
        public string Message { get; set; }
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}