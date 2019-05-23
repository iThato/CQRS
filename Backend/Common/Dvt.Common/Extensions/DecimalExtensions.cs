using System;
using System.Globalization;

namespace Dvt.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal CalculatePercentage(this decimal sourceLeft, decimal sourceRight)
        {
            if (sourceRight == 0) return 0;
            return sourceLeft / sourceRight * 100.0m;
        }

        public static decimal CalculatePercentage(this decimal? sourceLeft, decimal? sourceRight)
        {
            if (!(sourceLeft.HasValue && sourceRight.HasValue)) return 0;
            if (sourceRight.Value == 0) return 0;
            return sourceLeft.Value.CalculatePercentage(sourceRight.Value);
        }

        public static decimal RoundTo(this decimal total, int decimalPlaces)
        {
            return Math.Round(total, decimalPlaces, MidpointRounding.AwayFromZero);
        }

        public static string NullIfZero(this decimal value)
        {
            if (value > -0.01m && value < 0.01m)
                return null;
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToCurrency(this decimal value)
        {
            return string.Format(ConstantsFormatValue.Currency, value);
        }
    }
}
