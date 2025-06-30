using Api.Commons.Services;
using Application.Commons.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Commons.Extensions.Services;

public static class InjectionExtension
{
    public static void AddInjectionExtension(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}