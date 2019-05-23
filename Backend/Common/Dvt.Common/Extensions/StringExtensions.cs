using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Dvt.Common.Extensions
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        [ContractAnnotation("null=>false")]
        public static bool NotNullOrEmptyTrimmed(this string source)
        {
            return !IsNullOrEmptyTrimmed(source);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("null=>false")]
        public static bool NotNullOrEmpty(this string source)
        {
            return !IsNullOrEmpty(source);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmptyTrimmed(this string source)
        {
            var result = source.IsNullOrEmpty();
            if (!result)
                result = source.Trim().Length == 0;
            return result;
        }

        public static string EmptyIfNullOrEmptyTrimmed(this string source)
        {
            return source.NotNullOrEmptyTrimmed() ? source : string.Empty;
        }

        public static string EmptyIfNull(this string source)
        {
            var result = string.Empty;
            if (source.NotNull())
                result = source;
            return result;
        }

        [Pure]
        public static string RemoveAllWhiteSpace(this string source)
        {
            var result = Regex.Replace(source, @"\s", string.Empty);
            return result;
        }

        [Pure]
        public static char ToChar(this string source)
        {
            return Convert.ToChar(source);
        }

        [Pure]
        public static string RemoveNewLineCharacters(this string source)
        {
            return source.Replace("\r\n", string.Empty);
        }

        [Pure]
        public static string RemoveCharacters(this string source, char[] charactersToRemove)
        {
// ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var character in charactersToRemove)
            {
                source = source.Replace(character.ToString(CultureInfo.InvariantCulture), string.Empty);
            }
            return source;
        }

        [Pure]
        public static string Truncate(this string value, int length)
        {
            if (value.IsNullOrEmpty())
                return string.Empty;
            if (length <= 0)
                return string.Empty;

            if (value.Length > length)
                value = value.Substring(0, length);
            return value;
        }

        [Pure]
        public static string Utf8ByteArrayToString(this byte[] source)
        {
            return new UTF8Encoding().GetString(source);
        }

        [Pure]
        public static byte[] StringToUtf8ByteArray(this string source)
        {
            return new UTF8Encoding().GetBytes(source);
        }

        [Pure]
        public static string ToTitleCase(this string source)
        {
            if (source.IsNullOrEmptyTrimmed()) return source;
            var words = source.Split(' ');
            var result = new List<string>();
            foreach (var word in words)
            {
                if (word.Length == 0 || AllCapitals(word))
                    result.Add(word);
                else if (word.Length == 1)
                    result.Add(word.ToUpper());
                else
                    result.Add(char.ToUpper(word[0]) + word.Remove(0, 1).ToLower());
            }

            return string.Join(" ", result);
        }

        [Pure]
        public static string CamelCaseToWords(this string input)
        {
            return Regex.Replace(input.FirstCharToUpper(), "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        private static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper() + string.Join(string.Empty, input.Skip(1));
        }

        [Pure]
        private static bool AllCapitals(string input)
        {
            return Regex.IsMatch(input, @"^[A-Z]+$");
        }

        public static string StripNonAsciiCharaters(this string source)
        {
            return Regex.Replace(source, @"[^\u0000-\u007F]", string.Empty);
        }

        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
                return string.Empty;
            var buffer = new StringBuilder(xml.Length);
            foreach (var c in xml)
                if (IsLegalXmlChar(c))
                    buffer.Append(c);
            return buffer.ToString();
        }

        private static bool IsLegalXmlChar(int character)
        {
            return
                    character == 0x9 /* == '\t' == 9   */           ||
                    character == 0xA /* == '\n' == 10  */           ||
                    character == 0xD /* == '\r' == 13  */           ||
                    (character >= 0x20 && character <= 0xD7FF) ||
                    (character >= 0xE000 && character <= 0xFFFD) ||
                    (character >= 0x10000 && character <= 0x10FFFF);
        }
    }
}
