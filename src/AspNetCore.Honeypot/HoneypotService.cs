using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

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
    public bool IsTrapped(HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue(HttpContextItemName, out object? value) == false)
        {
            bool trapped = false;

            if (httpContext.Request.HasFormContentType == false)
            {
                trapped = true;
            }

            if (trapped == false && Options.IsFieldCheckEnabled)
            {
                //check fields
                trapped = httpContext.Request.Form.Any(x => Options.IsFieldName(x.Key) && x.Value.Any(v => string.IsNullOrEmpty(v) == false));
            }

            if (trapped == false && Options.IsTimeCheckEnabled)
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
