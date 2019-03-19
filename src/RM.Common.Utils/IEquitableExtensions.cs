using System;
using System.Collections.Generic;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="IEquatable{T}" />.
    /// </summary>
    public static class IEquitableExtensions
    {
        #region IsOneOf extensions
        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T item, T item1, T item2) where T : IEquatable<T>
        {
            if (item == null) return item1 == null || item2 == null;

            return item.Equals(item1) || item.Equals(item2);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to <paramref name="item1"/> or <paramref name="item2"/>.
        /// </summary>
        public static bool IsOneOf<T>(this T? item, T item1, T item2) where T : struct, IEquatable<T>
        {
            if (item == null) return false;

            var value = item.Value;
            return value.Equals(item1) || value.Equals(item2);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T item, T item1, T item2, T item3) where T : IEquatable<T>
        {
            if (item == null) return item1 == null || item2 == null || item3 == null;

            return item.Equals(item1) || item.Equals(item2) || item.Equals(item3);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T? item, T item1, T item2, T item3) where T : struct, IEquatable<T>
        {
            if (item == null) return false;

            var value = item.Value;
            return value.Equals(item1) || value.Equals(item2) || value.Equals(item3);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T item, T item1, T item2, T item3, T item4) where T : IEquatable<T>
        {
            if (item == null) return item1 == null || item2 == null || item3 == null || item4 == null;

            return item.Equals(item1) || item.Equals(item2) || item.Equals(item3) || item.Equals(item4);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T? item, T item1, T item2, T item3, T item4) where T : struct, IEquatable<T>
        {
            if (item == null) return false;

            var value = item.Value;
            return value.Equals(item1) || value.Equals(item2) || value.Equals(item3) || value.Equals(item4);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified <paramref name="items"/>.
        /// </summary>
        public static bool IsOneOf<T>(this T item, params T[] items) where T : IEquatable<T>
        {
            if (items == null) return false;
            return item == null ? items.Any(x => x == null) : items.Any(item.Equals);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified <paramref name="items"/>.
        /// </summary>
        public static bool IsOneOf<T>(this T? item, params T[] items) where T : struct, IEquatable<T>
        {
            if (item == null || items == null) return false;

            var value = item.Value;
            return items.Any(value.Equals);
        }

        /// <summary>
        /// Determines whether provided <paramref name="item"/> is equal to one of the specified items.
        /// </summary>
        public static bool IsOneOf<T>(this T item, IEnumerable<T> items) where T : IEquatable<T>
        {
            if (items == null) return false;
            return item == null ? items.Any(x => x == null) : items.Any(item.Equals);
        }
        #endregion
    }
}