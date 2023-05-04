using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Honeypot;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// AddHoneypot
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
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
    /// IsHoneypotTrapped
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static bool IsHoneypotTrapped(this HttpContext httpContext)
    {
        HoneypotService service = httpContext.RequestServices.GetRequiredService<HoneypotService>();

        return service.IsTrapped(httpContext);
    }
}
