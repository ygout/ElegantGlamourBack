using System.Collections.Generic;

namespace ElegantGlamour.Core.Responses
{
    public interface IListResponse<T> : IResponse
    {
        IEnumerable<T> Data { get; set; }
    }
}