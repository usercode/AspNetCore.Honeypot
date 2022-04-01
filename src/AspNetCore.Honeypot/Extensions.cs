using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace AspNetCore.Honeypot;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
    public static IServiceCollection AddHoneypot(this IServiceCollection services)
    {
        return AddHoneypot(services, x => { });
    }

    public static IServiceCollection AddHoneypot(this IServiceCollection services, Action<HoneypotOptions> options)
    {
        services.Configure(options);
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
