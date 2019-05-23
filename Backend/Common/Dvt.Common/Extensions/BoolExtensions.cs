using System;

namespace Dvt.Common.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToYesNo(this bool booleanValue)
        {
            return booleanValue ? "Yes" : "No";
        }

        public static bool ToBool(this string source)
        {
            if (source.IsNullOrEmptyTrimmed()) return false;

            var boolValue = source.Trim().ToLower();
            return boolValue == "yes" || boolValue == "true" || boolValue == "1" || boolValue == "y";
        }

        public static byte ToByte(this bool source)
        {
            return Convert.ToByte(source);
        }
    }
}
