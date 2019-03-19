using System;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for numbers.
    /// </summary>
    public static class NumbersExtensions
    {
        /// <summary>
        /// Checks whether the specified <paramref name="number"/> is odd.
        /// </summary>
        public static bool IsOdd(this int number) => number % 2 != 0;

        /// <summary>
        /// Checks whether the specified <paramref name="number"/> is even.
        /// </summary>
        public static bool IsEven(this int number) => number % 2 == 0;

        /// <summary>
        /// Checks whether the specified <paramref name="number"/> is positive.
        /// </summary>
        public static bool IsPositive(this int number) => number > 0;

        /// <summary>
        /// Checks whether the specified <paramref name="number"/> is negative.
        /// </summary>
        public static bool IsNegative(this int number) => number < 0;

        /// <summary>
        /// Checks whether the specified <paramref name="source"/> starts with specified <paramref name="value"/>.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="ignoreSign">Determines whether to ignore sign or not.</param>
        public static bool StartsWith(this short source, short value, bool ignoreSign = true) => StartsWith((long)source, value, ignoreSign);
        
        /// <summary>
        /// Checks whether the specified <paramref name="source"/> starts with specified <paramref name="value"/>.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="ignoreSign">Determines whether to ignore sign or not.</param>
        public static bool StartsWith(this int source, int value, bool ignoreSign = true) => StartsWith((long)source, value, ignoreSign);

        /// <summary>
        /// Checks whether the specified <paramref name="source"/> starts with specified <paramref name="value"/>.
        /// </summary>
        /// <param name="source">The source number.</param>
        /// <param name="value">The value to check.</param>
        /// <param name="ignoreSign">Determines whether to ignore sign or not.</param>
        public static bool StartsWith(this long source, long value, bool ignoreSign = true)
        {
            if (source == value) return true;
            if (value == 0) return source == 0;
            if (source == 0) return value == 0;
            // check whether source and value has different signs
            if (!ignoreSign && ((source > 0 && value < 0) || (source < 0 && value > 0))) return false;

            source = Math.Abs(source);
            value = Math.Abs(value);

            while (source > value) source /= 10;
            return source == value;
        }
    }
}