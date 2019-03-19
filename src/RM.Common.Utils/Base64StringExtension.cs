using System;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for Base64 convertions.
    /// </summary>
    public static class Base64StringExtension
    {
        /// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.</summary>
        /// <param name="data">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation, in base 64, of the contents of <paramref name="data"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="data"/> is null.</exception>
        public static string ToBase64String(this byte[] data) => Convert.ToBase64String(data);

        /// <summary>Converts the specified string, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array.</summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="str"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="str"/> is null.</exception>
        /// <exception cref="T:System.FormatException">The length of <paramref name="str"/>, ignoring white-space characters, is not zero or a multiple of 4.   -or-   The format of <paramref name="str">s</paramref> is invalid. <paramref name="str">s</paramref> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
        public static byte[] FromBase64String(this string str) => Convert.FromBase64String(str);
    }
}