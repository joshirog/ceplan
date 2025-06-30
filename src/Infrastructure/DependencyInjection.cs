using System.Net.Http.Headers;
using System.Net.Mime;
using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Domain.Entities;
using Infrastructure.Managers.Identity;
using Infrastructure.Persistences.Contexts;
using Infrastructure.Services;
using Infrastructure.Workers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(ConfigurationConstant.GetConnectionString, 
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
                .UseQueryTrackingBehavior(
                    QueryTrackingBehavior.NoTracking
                )
                .LogTo(Console.WriteLine, LogLevel.Warning)
        );

        services
            .AddIdentity<User, Role>(
                options =>
                {
                    options.User.RequireUniqueEmail = ConfigurationConstant.GetIdentityRequireUniqueEmail;
                    options.Lockout.AllowedForNewUsers = false;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(ConfigurationConstant.GetIdentityDefaultLockoutTimeSpan);
                    options.Lockout.MaxFailedAccessAttempts = ConfigurationConstant.GetIdentityMaxFailedAccessAttempts;
                    options.Password.RequiredLength = ConfigurationConstant.GetIdentityPasswordRequiredLength;
                    options.Password.RequiredUniqueChars = ConfigurationConstant.GetIdentityPasswordRequiredUniqueChars;
                    options.Password.RequireDigit = ConfigurationConstant.GetIdentityPasswordRequireDigit;
                    options.Password.RequireNonAlphanumeric = ConfigurationConstant.GetIdentityPasswordRequireNonAlphanumeric;
                    options.Password.RequireUppercase = ConfigurationConstant.GetIdentityPasswordRequireUppercase;
                    options.SignIn.RequireConfirmedEmail = ConfigurationConstant.GetIdentitySignInRequireConfirmedEmail;
                    options.Tokens.EmailConfirmationTokenProvider = ConfigurationConstant.GetIdentityEmailConfirmationTokenProvider;
                })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<IdentityCustomError>()
            .AddTokenProvider<IdentityEmailToken<User>>(ConfigurationConstant.GetIdentityEmailConfirmationTokenProvider)
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(ConfigurationConstant.GetIdentityTokenLifespan));
        
        services.AddLazyCache();
        
        services.AddScoped(provider => (IApplicationDbContext)provider.GetService<ApplicationDbContext>());
        
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IJsonSerializerService, JsonSerializerService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddHostedService<InitializeSeedWorker>();

        services.AddHttpClient("SendInBlue", c =>
        {
            c.BaseAddress = new Uri(ConfigurationConstant.GetSendInBlueHost);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            c.DefaultRequestHeaders.Add("api-key", ConfigurationConstant.GetSendInBlueApiKey);
        });
    }
}