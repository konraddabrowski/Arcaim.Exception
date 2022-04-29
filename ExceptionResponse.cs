using System.Net;

namespace Arcaim.Exception
{
  public class ExceptionResponse
  {
    public object Response { get; }
    public HttpStatusCode StatusCode { get; }
    public string Source { get; }

    public ExceptionResponse(object response, HttpStatusCode statusCode, string source)
      => (Response, StatusCode, Source) = (response, statusCode, source);
  }
}
