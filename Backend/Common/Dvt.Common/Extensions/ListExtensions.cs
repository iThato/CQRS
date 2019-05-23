using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Dvt.Common.Extensions
{
    public static class ListExtensionMethods
    {
        public static bool Match<T>(this IList<T> sourceList, Predicate<T> predicate)
        {
            if (sourceList.IsNull()) return false;
            if (predicate.IsNull()) return false;
            return sourceList.Any(s => predicate(s));
        }

        public static string Concatenate<T>(this IEnumerable<T> items, string delimiter, Converter<T, string> converter)
        {
            if (items.IsNull()) return string.Empty;
            delimiter.ThrowIfNull("delimiter");
            converter.ThrowIfNull("converter");
            var builder = new StringBuilder();
            foreach (var item in items)
            {
                builder.Append(converter(item));
                builder.Append(delimiter);
            }
            if (builder.Length > 0)
                builder.Length = builder.Length - delimiter.Length;

            return builder.ToString();
        }

        public static bool HasItems<T>([NoEnumeration] this IEnumerable<T> items)
        {
            return !items.IsNull() && items.Any();
        }

        public static bool HasNoItems<T>(this IEnumerable<T> items)
        {
            return !items.HasItems();
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items.IsNull()) return;
            if (action.IsNull()) return;
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
        {
            if (collection.IsNull()) return;
            if (items.IsNull()) return;
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        ///     Returns an empty enumeration if the <paramref name="source" /> is null.
        ///     Otherwise, returns the <paramref name="source" />.
        /// </summary>
        /// <param name="source" this="true">The enumerable to check if it's null</param>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
