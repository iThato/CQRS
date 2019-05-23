using System;

namespace Dvt.Common.Extensions
{
    public static class DateExtensions
    {
        public const string FormatIso8601Date = "yyyy-MM-dd";
        public const string FormatIso8601 = FormatIso8601Date + "THH:mm:ss.fffffffzzz";

        public static bool IsMinValue(this DateTime source)
        {
            return source == DateTime.MinValue;
        }

        public static bool IsMaxValue(this DateTime source)
        {
            return source == DateTime.MaxValue;
        }

        public static bool IsMinOrMaxValue(this DateTime source)
        {
            return source.IsMinValue() || source.IsMaxValue();
        }

        public static DateTime EndOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month));
        }

        public static DateTime StartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static bool IsWeekend(this DateTime source)
        {
            return source.DayOfWeek == DayOfWeek.Saturday ||
                   source.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsLeapYear(this DateTime date)
        {
            return date.Year%4 == 0 && (date.Year%100 != 0 || date.Year%400 == 0);
        }

        public static string ToIso8601(this DateTime date, bool stripTime = false)
        {
            return stripTime ? date.Date.ToString(FormatIso8601) : date.ToString(FormatIso8601);
        }

        public static string ToIso8601Date(this DateTime date)
        {
            return date.ToString(FormatIso8601Date);
        }

        public static DateTime ZeroMilliseconds(this DateTime date)
        {
            return new DateTime(date.Ticks/10000000*10000000, date.Kind);
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            return CalculateAge(DateTime.Today, birthDate);
        }

        //Internal method to all unit tests to override current date
        internal static int CalculateAge(DateTime currentDate, DateTime birthDate)
        {
            var age = currentDate.Year - birthDate.Year;
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day)) age--;

            return age;
        }
    }
}
