using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCore.Honeypot
{
    /// <summary>
    /// HoneypotService
    /// </summary>
    class HoneypotService
    {
        public const string HttpContextItemName = "AspNetCore.Honeypot.IsHoneypotTrapped";

        public HoneypotService()
        {

        }

        /// <summary>
        /// IsTrapped
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public bool IsTrapped(HttpContext httpContext)
        {
            if (httpContext.Items.TryGetValue(HttpContextItemName, out object value) == false)
            {
                HoneypotSettings settings = httpContext.RequestServices.GetRequiredService<HoneypotSettings>();

                bool trapped = httpContext.Request.Form
                                                    .Any(x => settings.IsFieldName(x.Key) && x.Value.Any(v => string.IsNullOrEmpty(v) == false));
                
                httpContext.Items.Add(HttpContextItemName, trapped);

                return trapped;
            }
            else
            {
                return (bool)value;
            }
        }

        //public IHtmlContent BuildTags()
        //{

        //}
    }
}
