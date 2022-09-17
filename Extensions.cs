using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Exception
{
  public static class Extensions
  {
    public static IServiceCollection AddExceptionMapper<T>(this IServiceCollection services)
      where T : IExceptionToResponseMapper
    {
      services.AddTransient<ErrorHandlerMiddleware>();
      var mapper = System.Activator.CreateInstance(typeof(T)) as IExceptionToResponseMapper;
      if (mapper is not null)
      {
        services.AddSingleton<IExceptionToResponseMapper>(mapper);
      }

      return services;
    }
  }
}