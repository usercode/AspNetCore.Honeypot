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

        public HoneypotSettings(String prefix)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// Prefix
        /// </summary>
        public String Prefix { get; set; }
        
        internal bool IsFieldName(String name)
        {
            return name.StartsWith($"{Prefix}");
        }

        internal String GetFieldName(String name)
        {
            return $"{Prefix}{name}";
        }

        internal String GetTimeFieldName()
        {
            return $"{Prefix}_time";
        }
    }
}
