using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotAttribute
/// </summary>
public class HoneypotAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        bool isTrapped = context.HttpContext.IsHoneypotTrapped();

        if (isTrapped == true)
        {
            context.Result = new ContentResult() { Content = "bot detection", ContentType = "text/plain", StatusCode = StatusCodes.Status200OK };
        }
    }
}
