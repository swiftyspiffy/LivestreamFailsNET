using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LivestreamFailsNET.Enums
{
    public enum Order
    {
        [Description("hot")]
        HOT,
        [Description("new")]
        NEW,
        [Description("top")]
        TOP,
        [Description("random")]
        RANDOM
    }

    internal static class OrderEnumExtensions
    {
        internal static string ToDescriptionString(this Order val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
