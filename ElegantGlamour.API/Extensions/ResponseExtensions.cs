using System.Net;
using ElegantGlamour.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ElegantGlamour.Api.Extensions
{
    public static class ResponseExtensions
    {
        public static ActionResult ToHttpResponse(this Response response)
        {
            var status = response.DidError ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
        public static ActionResult ToHttpResponse<TModel>(this SingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;
            if (response.DidError)
            {
                status = HttpStatusCode.InternalServerError;
                response.Message = "Internal server error";
            }
            else if (response.Data == null)
            {
                status = HttpStatusCode.NotFound;
                response.Message = "Not Found";
            }
            return new ObjectResult(response)
            {
                StatusCode = (int)status,

            };
        }
        public static ActionResult ToHttpResponse<TModel>(this ListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (response.DidError)
            {
                status = HttpStatusCode.InternalServerError;
                response.Message = "Internal server error";
            }

            else if (response.Data == null)
            {
                status = HttpStatusCode.NoContent;
                response.Message = "Not Content";
            }
            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }
}