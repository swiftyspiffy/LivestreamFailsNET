using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LivestreamFailsNET.Enums
{
    internal enum Mode
    {
        [Description("streamer")]
        STREAMER,
        [Description("game")]
        GAME,
        [Description("standard")]
        STANDARD
    }

    internal static class ModeEnumExtensions
    {
        internal static string ToDescriptionString(this Mode val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
