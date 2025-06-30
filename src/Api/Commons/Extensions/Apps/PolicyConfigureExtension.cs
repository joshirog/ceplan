using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api.Commons.Extensions.Apps;

public static class PolicyConfigureExtension
{
    public static void AddPolicyConfigure(this WebApplication app)
    {
        app.Use((context, next) =>
        {
            context.Response.Headers.Append("X-Frame-Options", "Deny");
            context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
            context.Response.Headers.Append("Permissions-Policy", "geolocation=(), midi=(), camera=(),usb=(), magnetometer=(), sync-xhr=(), microphone=(), camera=(), gyroscope=(), payment=()");
            context.Response.Headers.Append("Content-Security-Policy", "default-src https:; style-src https:; img-src https: data:; font-src https: data:; script-src https:; connect-src https:; frame-ancestors https:; form-action https:; base-uri https:; object-src 'none'");
            context.Response.Headers.Append("Expect-CT", "max-age=0");
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            return next.Invoke();
        });
    }
}