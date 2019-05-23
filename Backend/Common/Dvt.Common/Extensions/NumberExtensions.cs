using System;

namespace Dvt.Common.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        ///     Will iterate using a for for loop from 0 to index, calling the action supplied
        /// </summary>
        /// <param name="index">Number of times to iterate</param>
        /// <param name="action">Action method to be invoked</param>
        /// <code>
        /// Code Example:
        ///
        ///    10.Times (index => Console.WriteLine (index));
        ///
        ///    or
        ///
        ///    10.Times(Console.WriteLine);
        /// </code>
        public static void Times(this int index, Action<int> action)
        {
            for (var i = 0; i < index; i++)
                action(i);
        }

        private static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var seconds = Convert.ToDouble(unixTime);
            return epoch.AddSeconds(seconds);
        }

        /// <summary>
        ///     Takes a epoch date and will convert it to a .NET DateTime
        /// </summary>
        /// <returns>
        ///     Date converted to a .NET DateTime
        ///     If no date is supplied the will return DateTime.Min
        /// </returns>
        public static DateTime FromEpochDate(this long? epochDate)
        {
            if (!epochDate.HasValue) return DateTime.MinValue;
            return epochDate.Value.FromEpochDate();
        }

        public static DateTime FromEpochDate(this long epochDate)
        {
            return epochDate.FromUnixTime();
        }

        public static long ToEpochDate(this DateTime source)
        {
            return (long) (source - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static readonly string[] _sizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(this long value, int precision = 1)
        {
            if (value < 0)
            {
                return "-" + SizeSuffix(-value);
            }
            if (value == 0)
            {
                return "0"; }

            var i = 0;
            var currentCalc = (decimal)value;
            while (Math.Round(currentCalc / 1024) >= 1)
            {
                currentCalc /= 1024;
                i++;
            }
            var formatTemplate = $"{{0:n{precision}}}" + " {1}";
            return string.Format(formatTemplate, currentCalc, _sizeSuffixes[i]);
        }
    }
}
