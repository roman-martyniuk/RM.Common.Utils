using System;
using System.Globalization;
using System.Linq.Expressions;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="decimal" />.
    /// </summary>
    public static class DecimalExtensions
    {
        private delegate int GetDecimalPlacesDelegate(ref decimal value);
        private static readonly GetDecimalPlacesDelegate _getDecimalPlaces = CreateGetDecimalPlacesDelegate();
        private static readonly NumberFormatInfo _defaultFormatProvider = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

        static DecimalExtensions()
        {
            _defaultFormatProvider.NumberGroupSeparator = "";
            _defaultFormatProvider.NumberDecimalSeparator = ".";
        }

        /// <summary>
        /// Returns a number of decimal places in the specified <paramref name="number"/>.
        /// </summary>
        //This is a fast version of (decimal.GetBits(number)[3] & ~int.MinValue) >> 16.
        public static int GetDecimalPlaces(this decimal number) => _getDecimalPlaces(ref number);

        // creates method "ref value => (value.flags & ~int.MinValue) >> 16"
        private static GetDecimalPlacesDelegate CreateGetDecimalPlacesDelegate()
        {
            var value = Expression.Parameter(typeof(decimal).MakeByRefType(), "value");

            var digits = Expression.RightShift(
                Expression.And(Expression.Field(value, "flags"), Expression.Constant(~int.MinValue, typeof(int))),
                Expression.Constant(16, typeof(int)));

            return Expression.Lambda<GetDecimalPlacesDelegate>(digits, value).Compile();
        }

        /// <summary>
        /// Converts specified <paramref name="value"/> to its string representation.
        /// </summary>
        public static string Format(this decimal value, byte decimalPlaces = 2, string groupSeparator = "", string decimalSeparator = ".", string prefix = "", string suffix = "", bool forceSign = false)
        {
            var provider = GetFormatProvider(groupSeparator, decimalSeparator);
            var format = decimalPlaces == 2 ? "N2" : "N" + decimalPlaces;

            var result = value.ToString(format, provider);
            if (forceSign && value > 0m) result = '+' + result;
            if (!string.IsNullOrEmpty(prefix)) result = prefix + result;
            if (!string.IsNullOrEmpty(suffix)) result += suffix;

            return result;
        }

        /// <summary>
        /// Converts specified <paramref name="value"/> to its string representation.
        /// </summary>
        public static string Format(this decimal? value, byte decimalPlaces = 2, string groupSeparator = "", string decimalSeparator = ".", string prefix = "", string suffix = "", bool forceSign = false)
        {
            return value?.Format(decimalPlaces, groupSeparator, decimalSeparator, prefix, suffix, forceSign);
        }

        /// <summary>
        /// Converts specified <paramref name="value"/> to its string representation.
        /// </summary>
        public static string Format(this double value, byte decimalPlaces = 2, string groupSeparator = "", string decimalSeparator = ".", string prefix = "", string suffix = "", bool forceSign = false)
        {
            var provider = GetFormatProvider(groupSeparator, decimalSeparator);
            var format = decimalPlaces == 2 ? "N2" : "N" + decimalPlaces;

            var result = value.ToString(format, provider);
            if (forceSign && value > 0.0) result = '+' + result;
            if (!string.IsNullOrEmpty(prefix)) result = prefix + result;
            if (!string.IsNullOrEmpty(suffix)) result += suffix;

            return result;
        }

        /// <summary>
        /// Converts specified <paramref name="value"/> to its string representation.
        /// </summary>
        public static string Format(this double? value, byte decimalPlaces = 2, string groupSeparator = "", string decimalSeparator = ".", string prefix = "", string suffix = "", bool forceSign = false)
        {
            return value?.Format(decimalPlaces, groupSeparator, decimalSeparator, prefix, suffix, forceSign);
        }

        private static NumberFormatInfo GetFormatProvider(string groupSeparator, string decimalSeparator)
        {
            if (groupSeparator == "" && decimalSeparator == ".") return _defaultFormatProvider;

            var provider = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
            provider.NumberGroupSeparator = groupSeparator;
            provider.NumberDecimalSeparator = decimalSeparator;

            return provider;
        }

        /// <summary>
        /// Trims specified <paramref name="value"/> to the specified number of <paramref name="decimalPlaces"/>.
        /// </summary>
        public static decimal Trim(this decimal value, int decimalPlaces = 2)
        {
            Ensure.IsBetween(decimalPlaces, 0, 18, nameof(decimalPlaces));

            var pow = (long)Math.Pow(10, decimalPlaces);
            return Math.Truncate(value * pow) / pow;
        }
    }
}