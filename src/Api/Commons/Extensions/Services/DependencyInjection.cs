using Microsoft.Extensions.DependencyInjection;

namespace Api.Commons.Extensions.Services;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddAuthenticationExtension();
        services.AddControllerExtension();
        services.AddCorsExtension();
        services.AddSwaggerExtension();
        services.AddInjectionExtension();
    }
}