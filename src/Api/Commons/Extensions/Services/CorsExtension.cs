using Microsoft.Extensions.DependencyInjection;

namespace Api.Commons.Extensions.Services;

public static class CorsExtension
{
    public static void AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("_allowSpecificOrigins",
                cors =>
                {
                    cors
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}