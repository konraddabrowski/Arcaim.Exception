using System.Net;

namespace Arcaim.Exception
{
    public class ExceptionResponse
    {
        public object Response { get; }//
        public HttpStatusCode StatusCode { get; }

        public ExceptionResponse(object response, HttpStatusCode statusCode)
            => (Response, StatusCode) = (response, statusCode);
    }
}
