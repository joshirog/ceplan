using System.Linq;
using Api.Commons.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Commons.Extensions.Services;

public static class ControllerExtension
{
    public static void AddControllerExtension(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
            options.Conventions.Add(new SwaggerConventions());
        });

        //services.AddControllersWithViews()
        //.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        services.AddRouting(options => options.LowercaseUrls = true);
        
        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
        });
            
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    }
    
    private sealed class SwaggerConventions : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var definitions = "test";
            
            if (controller.ControllerType.Namespace is not null)
            {
                if (controller.ControllerType.Namespace.Contains("auth"))
                    definitions = "auth";
                if (controller.ControllerType.Namespace.Contains("web"))
                    definitions = "web";
                if (controller.ControllerType.Namespace.Contains("app"))
                    definitions = "app";
                if (controller.ControllerType.Namespace.Contains("share"))
                    definitions = "share";
            }

            controller.ApiExplorer.GroupName = definitions;
        }
    }
}