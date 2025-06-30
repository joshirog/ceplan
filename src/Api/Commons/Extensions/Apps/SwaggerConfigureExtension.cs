using Microsoft.AspNetCore.Builder;

namespace Api.Commons.Extensions.Apps;

public static class SwaggerConfigureExtension
{
    public static void AddConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/auth/swagger.json", "auth");
            c.SwaggerEndpoint("/swagger/web/swagger.json", "web");
            c.SwaggerEndpoint("/swagger/app/swagger.json", "app");
            c.SwaggerEndpoint("/swagger/share/swagger.json", "share");
        });
    }
}