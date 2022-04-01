using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotService
/// </summary>
class HoneypotService
{
    public const string HttpContextItemName = "AspNetCore.Honeypot.IsHoneypotTrapped";

    public HoneypotService(IOptions<HoneypotOptions> options)
    {
        Options = options.Value;
    }

    /// <summary>
    /// Options
    /// </summary>
    private HoneypotOptions Options { get; }

    /// <summary>
    /// IsTrapped
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public bool IsTrapped(HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue(HttpContextItemName, out object? value) == false)
        {
            bool trapped = false;

            if (Options.EnableFieldCheck)
            {
                //check fields
                trapped = httpContext.Request.Form.Any(x => Options.IsFieldName(x.Key) && x.Value.Any(v => string.IsNullOrEmpty(v) == false));
            }

            if (Options.EnableTimeCheck && trapped == false)
            {
                //check time
                if (httpContext.Request.Form.TryGetValue(Options.TimeFieldName, out StringValues timeValues))
                {
                    if (timeValues.Any())
                    {
                        TimeSpan diff = DateTime.UtcNow - new DateTime(long.Parse(timeValues.First()), DateTimeKind.Utc);

                        trapped = diff < Options.MinTimeDuration;
                    }
                }
            }

            httpContext.Items.Add(HttpContextItemName, trapped);

            return trapped;
        }
        else
        {
            return (bool)value;
        }
    }
}
