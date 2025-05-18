using andreiolaru.site.discord.bridge.Domain.BindingObjects;
using Microsoft.Extensions.Options;

namespace andreiolaru.site.discord.bridge.Middlewares;

public class ApiKeyMiddleware(RequestDelegate next, IOptionsMonitor<AppSettings> _settings)
{

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue("X-Api-Key", out var providedKey);
        
        if (!providedKey.Equals(_settings.CurrentValue.ApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            
            await context.Response.WriteAsync("Unauthorized");
            
            return;
        }

        await next(context);
    }
}