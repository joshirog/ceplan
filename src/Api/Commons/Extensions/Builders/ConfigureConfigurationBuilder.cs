using Application.Commons.Constants;
using Microsoft.AspNetCore.Builder;

namespace Api.Commons.Extensions.Builders;

public static class ConfigureConfigurationBuilder
{
    public static void AddConfigurationBuilder(this WebApplicationBuilder builder)
    {
#pragma warning disable ASP0013
        builder.Host.ConfigureAppConfiguration((_, configuration) =>
        {
            ConfigurationConstant.Load(configuration.Build());
        });
#pragma warning restore ASP0013
    }
}