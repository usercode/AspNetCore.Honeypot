using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Honeypot
{
    /// <summary>
    /// HoneypotSettings
    /// </summary>
    public class HoneypotSettings
    {
        public HoneypotSettings()
            : this("hpf_")
        {

        }

        public HoneypotSettings(string prefix)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// Prefix
        /// </summary>
        public string Prefix { get; set; }
        
        internal bool IsFieldName(string name)
        {
            return name.StartsWith($"{Prefix}");
        }

        internal string GetFieldName(string name)
        {
            return $"{Prefix}{name}";
        }

        internal string GetTimeFieldName()
        {
            return $"{Prefix}_time";
        }
    }
}
