using Application.Commons.Constants;
using Microsoft.AspNetCore.Builder;

namespace Api.Commons.Extensions.Apps;

public static class EnvironmentConfigureExtension
{
    public static void AddEnvironmentConfigure(this WebApplication app)
    {
        if (ConfigurationConstant.GetEnvironment.Equals("Production")) return;

        app.UseDeveloperExceptionPage();
        app.AddConfigureSwagger();
    }
}