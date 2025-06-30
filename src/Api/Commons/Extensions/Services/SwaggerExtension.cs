using System;
using System.Linq;
using System.Reflection;
using Application.Commons.Constants;
using Application.Features.Auth.SignIn.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Commons.Extensions.Services;

public static class SwaggerExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        SwaggerExamplesFromAssemblyOf(services);
        
        services.AddSwaggerGen(c =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
            c.EnableAnnotations();
            c.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                {
                    return new[] { api.GroupName };
                }

                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    return new[] { controllerActionDescriptor.ControllerName };
                }

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });
            
            c.ExampleFilters();
            
            c.OperationFilter<QueryParameterExampleFilter>();
            
            c.SwaggerDoc("auth", new OpenApiInfo { Title = $"{ConfigurationConstant.GetApplicationName} - auth", Version = "v1" });
            c.SwaggerDoc("web", new OpenApiInfo { Title = $"{ConfigurationConstant.GetApplicationName} - web", Version = "v1" });
            c.SwaggerDoc("app", new OpenApiInfo { Title = $"{ConfigurationConstant.GetApplicationName} - app", Version = "v1" });
            c.SwaggerDoc("share", new OpenApiInfo { Title = $"{ConfigurationConstant.GetApplicationName} - share", Version = "v1" });
        });
    }
    
    private static void SwaggerExamplesFromAssemblyOf(IServiceCollection services)
    {
        services.AddSwaggerExamplesFromAssemblyOf<SignInExample>();
    }
}

public class SwaggerQueryExampleAttribute : Attribute
{
    public string Example { get; }

    public SwaggerQueryExampleAttribute(string example)
    {
        Example = example;
    }
}

public class QueryParameterExampleFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parameters = context.MethodInfo.GetParameters();

        foreach (var parameter in parameters)
        {
            var exampleAttribute = parameter.GetCustomAttributes<SwaggerQueryExampleAttribute>().FirstOrDefault();

            if (exampleAttribute is null) 
                continue;
            
            var parameterName = parameter.Name;
            var example = exampleAttribute.Example;

            var queryParameter = operation.Parameters
                // ReSharper disable once RedundantEnumerableCastCall
                .OfType<OpenApiParameter>()
                .FirstOrDefault(p => p.Name == parameterName);

            if (queryParameter != null)
            {
                queryParameter.Example = new OpenApiString(example);
            }
        }
    }
}
