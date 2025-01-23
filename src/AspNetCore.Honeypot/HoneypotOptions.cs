namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotOptions
/// </summary>
public class HoneypotOptions
{
    /// <summary>
    /// EnableFieldCheck
    /// </summary>
    public bool IsFieldCheckEnabled { get; set; } = true;

    /// <summary>
    /// EnableTimeCheck
    /// </summary>
    public bool IsTimeCheckEnabled { get; set; } = true;

    /// <summary>
    /// PrefixFieldName
    /// </summary>
    public string PrefixFieldName { get; set; } = "hp_";

    /// <summary>
    /// TimeFieldName
    /// </summary>
    public string TimeFieldName { get; set; } = "_time";

    /// <summary>
    /// MinTimeDuration
    /// </summary>
    public TimeSpan MinTimeDuration { get; set; } = TimeSpan.FromSeconds(1);

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
