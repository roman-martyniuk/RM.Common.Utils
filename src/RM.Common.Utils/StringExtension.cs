using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>Determines whether two specified <see cref="string"/> objects have the same value (using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison).</summary>
        /// <param name="str1">The first string to compare, or null.</param>
        /// <param name="str2">The second string to compare, or null.</param>
        public static bool EqualsOrdinalIgnoreCase(this string str1, string str2) => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

        /// <summary>Determines whether two specified <see cref="string"/> objects have the same value (using <see cref="StringComparison.Ordinal"/> comparison).</summary>
        /// <param name="str1">The first string to compare, or null.</param>
        /// <param name="str2">The second string to compare, or null.</param>
        public static bool EqualsOrdinal(this string str1, string str2) => string.Equals(str1, str2, StringComparison.Ordinal);

        #region Contains
        /// <summary>Returns a value indicating whether a specified substring occurs (using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison) within this string.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        public static bool ContainsOrdinalIgnoreCase(this string source, string value)
        {
            Ensure.IsNotNull(source, nameof(source));
            Ensure.IsNotNull(value, nameof(value));

            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>Returns a value indicating whether a specified substring occurs (using <see cref="StringComparison.Ordinal"/> comparison) within this string.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        public static bool ContainsOrdinal(this string source, string value)
        {
            Ensure.IsNotNull(source, nameof(source));
            Ensure.IsNotNull(value, nameof(value));

            return source.IndexOf(value, StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs (using <see cref="StringComparison.OrdinalIgnoreCase"/> comparison) within this string.
        /// NOTE: Method is null-safe (<paramref name="source"/> and <paramref name="value"/> can be null). Returns false if <paramref name="source"/> or <paramref name="value"/> is null.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        public static bool ContainsOrdinalIgnoreCaseNullSafe(this string source, string value)
        {
            if (source == null || value == null) return false;

            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs (using <see cref="StringComparison.Ordinal"/> comparison) within this string.
        /// NOTE: Method is null-safe (<paramref name="source"/> and <paramref name="value"/> can be null). Returns false if <paramref name="source"/> or <paramref name="value"/> is null.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        public static bool ContainsOrdinalNullSafe(this string source, string value)
        {
            if (source == null || value == null) return false;

            return source.IndexOf(value, StringComparison.Ordinal) >= 0;
        }
        #endregion

        /// <summary>Indicates whether the specified string is null or an empty string.</summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>Indicates whether a specified string is null, empty, or consists only of white-space characters.</summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>Indicates whether a specified string is not null, empty, or consists only of white-space characters.</summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNotNullOrWhiteSpace(this string value) => !string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="byte"/> equivalent, and returns a converted number. Returns null if the conversion failed.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        public static byte? ToByteSafe(this string str)
        {
            if (str == null) return null;
            return byte.TryParse(str, out var value) ? (byte?)value : null;
        }

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="int"/> equivalent, and returns a converted number. Returns null if the conversion failed.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        public static int? ToIntSafe(this string str)
        {
            if (str == null) return null;
            return int.TryParse(str, out var value) ? (int?)value : null;
        }

        /// <summary>
        /// Tries to convert the string representation of a number to its <see cref="long"/> equivalent, and returns a converted number. Returns null if the conversion failed.
        /// </summary>
        /// <param name="str">A string that contains a number to convert.</param>
        public static long? ToLongSafe(this string str)
        {
            if (str == null) return null;
            return long.TryParse(str, out var value) ? (long?)value : null;
        }

        /// <summary>
        /// Trims string and if resulting string is empty, null is returned.
        /// </summary>
        /// <param name="str">A string to trim.</param>
        public static string TrimToNull(this string str)
        {
            Ensure.IsNotNull(str, nameof(str));

            str = str.Trim();
            return str.Length == 0 ? null : str;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="string"/> using the invariant culture formatting information.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        public static string ToStringInvariant<T>(this T source) where T : IConvertible
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="string"/> using the specified <paramref name="format"/> and the invariant culture formatting information.
        /// </summary>
        /// <param name="source">The source object to convert.</param>
        /// <param name="format">The format to use.</param>
        public static string ToStringInvariant<T>(this T source, string format) where T : IFormattable
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a substring which contains a specified number of contiguous elements from the start of the string
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="count">The specified number of contiguous elements from the start of the string</param>
        /// <returns>Returns a substring which contains a specified number of contiguous elements from the start of the string</returns>
        public static string First(this string source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count > source.Length) throw new ArgumentOutOfRangeException(nameof(count));

            return source.Substring(0, count);
        }

        /// <summary>
        /// Returns a substring which contains a specified number of contiguous elements from the end of the string
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="count">The specified number of contiguous elements from the end of the string</param>
        /// <returns>Returns a substring which contains a specified number of contiguous elements from the end of the string</returns>
        public static string Last(this string source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count > source.Length) throw new ArgumentOutOfRangeException(nameof(count));
            return source.Substring(source.Length - count);
        }

        #region StartsWithAny
        /// <summary>
        /// Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substring1">The first substring.</param>
        /// <param name="substring2">The second substring.</param>
        public static bool StartsWithAny(this string str, string substring1, string substring2)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substring1, nameof(substring1));
            Ensure.IsNotNull(substring2, nameof(substring2));

            return str.StartsWith(substring1) || str.StartsWith(substring2);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substring1">The first substring.</param>
        /// <param name="substring2">The second substring.</param>
        /// <param name="substring3">The third substring.</param>
        public static bool StartsWithAny(this string str, string substring1, string substring2, string substring3)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substring1, nameof(substring1));
            Ensure.IsNotNull(substring2, nameof(substring2));
            Ensure.IsNotNull(substring3, nameof(substring3));

            return str.StartsWith(substring1) || str.StartsWith(substring2) || str.StartsWith(substring3);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substrings">The substrings.</param>
        public static bool StartsWithAny(this string str, params string[] substrings)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substrings, nameof(substrings));

            return str.StartsWithAny(substrings.AsEnumerable());
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substrings">The substrings.</param>
        public static bool StartsWithAny(this string str, IEnumerable<string> substrings)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substrings, nameof(substrings));

            return substrings.Any(str.StartsWith);
        }
        #endregion

        #region EndsWithAny
        /// <summary>
        /// Determines whether the end of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substring1">The first substring.</param>
        /// <param name="substring2">The second substring.</param>
        public static bool EndsWithAny(this string str, string substring1, string substring2)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substring1, nameof(substring1));
            Ensure.IsNotNull(substring2, nameof(substring2));

            return str.EndsWith(substring1) || str.EndsWith(substring2);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substring1">The first substring.</param>
        /// <param name="substring2">The second substring.</param>
        /// <param name="substring3">The third substring.</param>
        public static bool EndsWithAny(this string str, string substring1, string substring2, string substring3)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substring1, nameof(substring1));
            Ensure.IsNotNull(substring2, nameof(substring2));
            Ensure.IsNotNull(substring3, nameof(substring3));

            return str.EndsWith(substring1) || str.EndsWith(substring2) || str.EndsWith(substring3);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substrings">The substrings.</param>
        public static bool EndsWithAny(this string str, params string[] substrings)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substrings, nameof(substrings));

            return str.EndsWithAny(substrings.AsEnumerable());
        }

        /// <summary>
        /// Determines whether the end of this string instance matches any of the specified strings.
        /// </summary>
        /// <param name="str">The source string to test.</param>
        /// <param name="substrings">The substrings.</param>
        public static bool EndsWithAny(this string str, IEnumerable<string> substrings)
        {
            Ensure.IsNotNull(str, nameof(str));
            Ensure.IsNotNull(substrings, nameof(substrings));

            return substrings.Any(str.EndsWith);
        }
        #endregion
    }
}