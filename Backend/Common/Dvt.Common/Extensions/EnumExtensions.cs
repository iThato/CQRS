using System;
using System.ComponentModel;

namespace Dvt.Common.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ParseEnum<TEnum>(this int value, TEnum defaultValue = default(TEnum))
         where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumValue = value.ToString();
            var result = enumValue.ParseEnum(defaultValue);
            // Note that IsDefined does not worked if the Enum types uses [Flags] attribute
            return Enum.IsDefined(typeof(TEnum), result) ? result : defaultValue;
        }

        public static TEnum ParseEnum<TEnum>(this string value, TEnum defaultValue = default(TEnum))
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");
            if (value.IsNullOrEmptyTrimmed())
                return defaultValue;
            return Enum.TryParse(value, true, out TEnum result) ? result : defaultValue;
        }

        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
