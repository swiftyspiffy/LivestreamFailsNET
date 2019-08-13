using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LivestreamFailsNET.Enums
{
    public enum Timeframe
    {
        [Description("day")]
        TODAY,
        [Description("week")]
        WEEK,
        [Description("month")]
        MONTH,
        [Description("year")]
        YEAR,
        [Description("all")]
        ALL_TIME
    }

    internal static class TimeframeEnumExtensions
    {
        internal static string ToDescriptionString(this Timeframe val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
