namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="bool" />.
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// Converts specified <paramref name="value"/> to lower-case string ("true" or false").
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>The converted value.</returns>
        public static string ToLowerString(this bool value) => value ? "true" : "false";

        /// <summary>
        /// Converts specified <paramref name="value"/> to lower-case string ("true" or false").
        /// NOTE: if <paramref name="value"/> is null an empty string is returned.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>The converted value.</returns>
        public static string ToLowerString(this bool? value)
        {
            if (value == null) return "";

            return value.Value ? "true" : "false";
        }
    }
}