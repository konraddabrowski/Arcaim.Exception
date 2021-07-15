using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Exception
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(System.Exception exception);
    }
}
