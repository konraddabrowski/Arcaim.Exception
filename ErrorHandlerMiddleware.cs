
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Arcaim.Exception
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

        public ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionToResponseMapper)
        {
            _exceptionToResponseMapper = exceptionToResponseMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_exceptionToResponseMapper is null)
            {
                await next(context);

                return;
            }

            try
            {
                await next(context);
            }
            catch (System.Exception exception)
            {
                //TODO: logger
                await this.HandlerErrorAsync(context, exception);
            }
        }

        private async Task HandlerErrorAsync(HttpContext context, System.Exception exception)
        {
            var exceptionResponse = _exceptionToResponseMapper.Map(exception);

            context.Response.StatusCode = exceptionResponse is not null
                ? (int)exceptionResponse.StatusCode
                : (int)HttpStatusCode.BadRequest;

            if (exceptionResponse?.Response == null)
            {
                await context.Response.WriteAsync(string.Empty, new CancellationToken());
            }
            else
            {
                context.Response.ContentType = "application/json";
                var text = JsonConvert.SerializeObject(exceptionResponse.Response);
                await context.Response.WriteAsync(text, new CancellationToken());
            }
        }
    }
}
