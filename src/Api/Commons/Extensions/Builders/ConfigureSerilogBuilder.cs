using System.IO;
using Application.Commons.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;

namespace Api.Commons.Extensions.Builders;

public static class ConfigureSerilogBuilder
{
    private static LoggingLevelSwitch BaseLevelSwitch { get; } = new();
    
    private static LoggingLevelSwitch MicrosoftLevelSwitch { get; } = new();
    
    private static LoggingLevelSwitch MicrosoftEntityFrameworkCoreLevelSwitch { get; } = new();
    
    private static LoggingLevelSwitch MicrosoftHostingLifetimeLevelSwitch { get; } = new();
    
    public static void AddSerilogBuilder(this WebApplicationBuilder builder)
    {
        const string template = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";
        
        BaseLevelSwitch.MinimumLevel = LogEventLevel.Information;
        MicrosoftLevelSwitch.MinimumLevel = LogEventLevel.Information;
        MicrosoftEntityFrameworkCoreLevelSwitch.MinimumLevel = LogEventLevel.Information;
        MicrosoftHostingLifetimeLevelSwitch.MinimumLevel = LogEventLevel.Information;
        
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .MinimumLevel.ControlledBy(BaseLevelSwitch)
            .MinimumLevel.Override("Microsoft", MicrosoftLevelSwitch)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", MicrosoftEntityFrameworkCoreLevelSwitch)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", MicrosoftHostingLifetimeLevelSwitch)
            .WriteTo.Console(outputTemplate: template)
            .WriteTo.Debug()
            .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), $"Logs/{ConfigurationConstant.GetApplicationName}-.log"),
                shared: false,
                rollingInterval: RollingInterval.Day,
                levelSwitch: BaseLevelSwitch,
                outputTemplate: template)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }
}