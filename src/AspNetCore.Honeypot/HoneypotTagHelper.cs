using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Honeypot
{
    /// <summary>
    /// HoneypotTagHelper
    /// </summary>
    public class HoneypotTagHelper : TagHelper
    {
        public HoneypotTagHelper(HoneypotSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Settings
        /// </summary>
        public HoneypotSettings Settings { get; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "div";

            String fieldName = Settings.GetFieldName(Name);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("name", fieldName);
            input.MergeAttribute("id", fieldName);
            input.MergeAttribute("type", "text");

            output.Content.AppendHtml(input);     
        }
    }
}
