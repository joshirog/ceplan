using System.Text;
using Application.Commons.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Commons.Extensions.Services;

public static class AuthenticationExtension
{
    public static void AddAuthenticationExtension(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        
        services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = ConfigurationConstant.GetJwtIssuer,
                    ValidAudience = ConfigurationConstant.GetJwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationConstant.GetJwtKey))
                };
            });
    }
}