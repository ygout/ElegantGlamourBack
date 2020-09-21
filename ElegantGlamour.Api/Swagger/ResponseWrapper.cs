using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElegantGlamour.Api.Swagger
{
    public class ResponseWrapper<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
