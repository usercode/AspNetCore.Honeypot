using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotFieldTagHelper
/// </summary>
public class HoneypotFieldTagHelper : TagHelper
{
    public HoneypotFieldTagHelper(IOptions<HoneypotOptions> options)
    {
        Name = "name";
        Options = options.Value;
    }

    /// <summary>
    /// Options
    /// </summary>
    public HoneypotOptions Options { get; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        output.TagMode = TagMode.SelfClosing;
        output.TagName = "input";

        string fieldName = Options.GetFieldName(Name);

        output.Attributes.Add("name", fieldName);
        output.Attributes.Add("id", fieldName);
        output.Attributes.Add("type", "text");
    }
}
