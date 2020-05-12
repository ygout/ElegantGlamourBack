using System.Collections.Generic;
using ElegantGlamour.Core.Responses;

namespace ElegantGlamour.Api.Responses
{
    public class ListResponse<T>
    {
        public string Message { get; set; }
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}