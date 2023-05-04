namespace AspNetCore.Honeypot;

/// <summary>
/// HoneypotOptions
/// </summary>
public class HoneypotOptions
{
    public HoneypotOptions()
        : this("hp_")
    {
        
    }

    public HoneypotOptions(string prefix)
    {
        IsFieldCheckEnabled = true;
        IsTimeCheckEnabled = true;
        PrefixFieldName = prefix; 
        TimeFieldName = "_time";
        MinTimeDuration = TimeSpan.FromSeconds(1);
    }

    /// <summary>
    /// EnableFieldCheck
    /// </summary>
    public bool IsFieldCheckEnabled { get; set; }

    /// <summary>
    /// EnableTimeCheck
    /// </summary>
    public bool IsTimeCheckEnabled { get; set; }

    /// <summary>
    /// PrefixFieldName
    /// </summary>
    public string PrefixFieldName { get; set; }

    /// <summary>
    /// TimeFieldName
    /// </summary>
    public string TimeFieldName { get; set; }

    /// <summary>
    /// MinTimeDuration
    /// </summary>
    public TimeSpan MinTimeDuration { get; set; }

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
