using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotTimeTagHelper
/// </summary>
public class HoneypotTimeTagHelper : TagHelper
{
    public HoneypotTimeTagHelper(IOptions<HoneypotOptions> options)
    {
        Options = options.Value;
    }

    /// <summary>
    /// Options
    /// </summary>
    public HoneypotOptions Options { get; }    

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        output.TagMode = TagMode.SelfClosing;
        output.TagName = "input";

        output.Attributes.Add("name", Options.TimeFieldName);
        output.Attributes.Add("id", Options.TimeFieldName);
        output.Attributes.Add("type", "hidden");
        output.Attributes.Add("value", DateTime.UtcNow.Ticks.ToString());
    }
}
