using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Honeypot;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Adds honeypot services.
    /// </summary>
    public static IServiceCollection AddHoneypot(this IServiceCollection services, Action<HoneypotOptions>? options = null)
    {
        if (options != null)
        {
            services.Configure(options);
        }

        services.AddTransient<HoneypotService>();

        return services;
    }

    /// <summary>
    /// Is honepot triggered?
    /// </summary>
    public static async Task<bool> IsHoneypotTriggeredAsync(this HttpContext httpContext)
    {
        HoneypotService service = httpContext.RequestServices.GetRequiredService<HoneypotService>();

        return await service.IsTriggeredAsync(httpContext);
    }
}
