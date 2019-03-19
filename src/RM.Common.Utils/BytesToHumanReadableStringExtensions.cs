using System;
using System.Globalization;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extensions for converting bytes number (e.g. file size) in the human readable format (max 3 digits) (1024B => 1 KB, 529283827B => 504 MB).
    /// </summary>
    public static class BytesToHumanReadableStringExtensions
    {
        private static readonly string[] _units = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

        /// <summary>
        /// Converts a number of bytes (size in bytes) to the human readable format using the specified <paramref name="formatProvider"/>.
        /// </summary>
        /// <param name="bytes">The number of bytes to format.</param>
        /// <param name="formatProvider">The format provider.</param>
        public static string BytesToHumanReadableString(this long bytes, IFormatProvider formatProvider)
        {
            Ensure.IsGreaterThanOrEqualToZero(bytes, nameof(bytes));

            const int TOTAL_DIGITS = 3;

            var size = (decimal)bytes;
            var unitsIndex = 0;
            while (size >= 1024 && unitsIndex < _units.Length - 1)
            {
                unitsIndex++;
                size /= 1024;
            }

            var integralPartDigits = GetDigitsCount((long)size);
            var decimalPlaces = Math.Max(0, TOTAL_DIGITS - integralPartDigits);
            size = size.Trim(decimalPlaces);

            // if units is 'B' (bytes) then format will be always without decimal part (F0)
            var format = "F" + (unitsIndex == 0 ? 0 : decimalPlaces);
            var units = _units[unitsIndex];

            return $"{size.ToString(format, formatProvider)} {units}";
        }

        /// <summary>
        /// Converts a number of bytes (size in bytes) to the human readable format.
        /// </summary>
        /// <param name="bytes">The number of bytes to format.</param>
        public static string BytesToHumanReadableString(this long bytes) => BytesToHumanReadableString(bytes, CultureInfo.CurrentCulture);

        /// <summary>
        /// Converts a number of bytes (size in bytes) to the human readable format using culture-independent (invariant) format provider.
        /// </summary>
        /// <param name="bytes">The number of bytes to format.</param>
        public static string BytesToHumanReadableStringInvariant(this long bytes) => BytesToHumanReadableString(bytes, CultureInfo.InvariantCulture);

        private static int GetDigitsCount(long value) => value == 0 ? 1 : (int)Math.Truncate(Math.Log10(value) + 1);
    }
}