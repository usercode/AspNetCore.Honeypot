using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Linq;

namespace AspNetCore.Honeypot
{
    /// <summary>
    /// HoneyFieldValidationAttribute
    /// </summary>
    public class HoneypotAttribute : ActionFilterAttribute, IFilterFactory
    {
        public HoneypotAttribute()
        {

        }

        public HoneypotAttribute(HoneypotSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Settings
        /// </summary>
        public HoneypotSettings Settings { get; }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return (IFilterMetadata)serviceProvider.GetService(typeof(HoneypotAttribute));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            bool isTrapped = new HoneypotService().IsTrapped(context.HttpContext);

            if(isTrapped == true)
            {
                context.Result = new ContentResult() { Content = "bot detection", ContentType = "text/plain", StatusCode = (int)HttpStatusCode.OK };
            }
        }
    }
}
