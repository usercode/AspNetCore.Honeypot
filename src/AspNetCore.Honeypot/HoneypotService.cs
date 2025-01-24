using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotService
/// </summary>
class HoneypotService
{
    public const string HttpContextItemName = "AspNetCore.Honeypot.IsHoneypotTriggered";

    public HoneypotService(IOptions<HoneypotOptions> options)
    {
        Options = options.Value;
    }

    /// <summary>
    /// Options
    /// </summary>
    private HoneypotOptions Options { get; }

    /// <summary>
    /// Is honeypot triggered?
    /// </summary>
    public async Task<bool> IsTriggeredAsync(HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue(HttpContextItemName, out object? value) == false)
        {
            bool triggered = false;

            if (httpContext.Request.HasFormContentType == false)
            {
                triggered = true;
            }

            IFormCollection form = await httpContext.Request.ReadFormAsync();

            if (triggered == false && Options.IsFieldCheckEnabled)
            {
                //check fields
                triggered = form.Any(x => Options.IsFieldName(x.Key) && x.Value.Any(v => string.IsNullOrEmpty(v) == false));
            }

            if (triggered == false && Options.IsTimeCheckEnabled)
            {
                //check time
                if (form.TryGetValue(Options.TimeFieldName, out StringValues timeValues))
                {
                    if (timeValues.Count > 0 && timeValues[0] is string timeValue)
                    {
                        if (long.TryParse(timeValue, out long time))
                        {
                            TimeSpan diff = DateTime.UtcNow - new DateTime(time, DateTimeKind.Utc);

                            triggered = diff < Options.MinResponseTime;
                        }
                        else
                        {
                            triggered = true; //time field doesn't contain long value.
                        }
                    }
                }
            }

            httpContext.Items.Add(HttpContextItemName, triggered);

            return triggered;
        }
        else
        {
            return (bool)value;
        }
    }
}
