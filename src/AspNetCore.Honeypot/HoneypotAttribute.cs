using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotAttribute
/// </summary>
public class HoneypotAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool triggered = await context.HttpContext.IsHoneypotTriggeredAsync();

        if (triggered == true)
        {
            context.Result = new ContentResult() { Content = "bot detection", ContentType = "text/plain", StatusCode = StatusCodes.Status200OK };
        }

        await base.OnActionExecutionAsync(context, next);
    }
}
