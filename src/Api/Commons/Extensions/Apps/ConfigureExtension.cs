using Microsoft.AspNetCore.Builder;

namespace Api.Commons.Extensions.Apps;

public static class ConfigureExtension
{
    public static void AddConfigure(this WebApplication app)
    {
        app.AddEnvironmentConfigure();
        app.UseHttpsRedirection();
        app.AddPolicyConfigure();
        app.UseRouting();
        app.UseCors("_allowSpecificOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
    }
}