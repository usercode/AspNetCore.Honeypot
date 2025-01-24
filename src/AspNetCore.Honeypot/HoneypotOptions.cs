namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotOptions
/// </summary>
public class HoneypotOptions
{
    /// <summary>
    /// Is field check enabled?
    /// </summary>
    public bool IsFieldCheckEnabled { get; set; } = true;

    /// <summary>
    /// Is time check enabled?
    /// </summary>
    public bool IsTimeCheckEnabled { get; set; } = true;

    /// <summary>
    /// Prefix for fields.
    /// </summary>
    public string PrefixFieldName { get; set; } = "hp_";

    /// <summary>
    /// Prefix for time fields.
    /// </summary>
    public string TimeFieldName { get; set; } = "_time";

    /// <summary>
    /// Minimal time for user response.
    /// </summary>
    public TimeSpan MinResponseTime { get; set; } = TimeSpan.FromSeconds(1);

    internal bool IsFieldName(string name)
    {
        return name.StartsWith($"{PrefixFieldName}");
    }

    internal string GetFieldName(string name)
    {
        return $"{PrefixFieldName}{name}";
    }

    internal string GetTimeFieldName()
    {
        return $"{PrefixFieldName}_time";
    }
}
