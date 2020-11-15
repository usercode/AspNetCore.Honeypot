using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace AspNetCore.Honeypot
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        public static IServiceCollection AddHoneypot(this IServiceCollection services)
        {
            return AddHoneypot(services, new HoneypotSettings());
        }

        public static IServiceCollection AddHoneypot(this IServiceCollection services, HoneypotSettings settings)
        {
            services.AddTransient<HoneypotAttribute>();
            services.AddTransient<HoneypotService>();
            services.AddSingleton(settings);

            return services;
        }

        /// <summary>
        /// IsHoneypotTrapped
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool IsHoneypotTrapped(this HttpContext httpContext)
        {
            return new HoneypotService().IsTrapped(httpContext);
        }
    }
}
