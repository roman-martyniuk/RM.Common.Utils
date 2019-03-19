using System;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents class for work with Unix time (timestamp).
    /// </summary>
    public static class UnixTime
    {
        /// <summary>
        /// The initial Unix time (1970-01-01 00:00:00).
        /// </summary>
        public static readonly DateTime Initial = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// The min Unix time (1901-12-13 20:45:52).
        /// </summary>
        public static readonly DateTime MinValue = Initial.AddSeconds(int.MinValue);

        /// <summary>
        /// The max Unix time (2038-01-19 3:14:07).
        /// </summary>
        public static readonly DateTime MaxValue = Initial.AddSeconds(int.MaxValue);

        /// <summary>
        /// Chechs whether the specified <paramref name="datetime"/> is a valid Unix date (is within <see cref="MinValue"/> and <see cref="MaxValue"/>).
        /// </summary>
        /// <param name="datetime">The date to check.</param>
        public static bool IsValidUnixDate(this DateTime datetime) => MinValue <= datetime && datetime <= MaxValue;

        /// <summary>
        /// Checks whether the specified <paramref name="datetime"/> is the initial Unix date (1970-01-01 00:00:00).
        /// </summary>
        /// <param name="datetime">The datetime to check.</param>
        public static bool IsInitialUnixTime(this DateTime datetime) => Initial == datetime;

        /// <summary>
        /// Converts the specified datetime to the Unix timestamp (the number of seconds since 1970-01-01 00:00:00).
        /// </summary>
        /// <param name="datetime">The datetime to convert.</param>
        public static int ToUnixTime(this DateTime datetime) => (int) (datetime - Initial).TotalSeconds;

        /// <summary>
        /// Converts the specified Unix timestamp (the number of seconds since 1970-01-01 00:00:00) to the datetime.
        /// </summary>
        /// <param name="unixTimestamp">The unix timestamp to convert.</param>
        public static DateTime FromUnixTime(this int unixTimestamp) => Initial.AddSeconds(unixTimestamp);

        /// <summary>
        /// Converts the specified Unix timestamp (the number of seconds since 1970-01-01 00:00:00) to the datetime.
        /// </summary>
        /// <param name="unixTimestamp">The unix timestamp to convert.</param>
        public static DateTime FromUnixTime(this long unixTimestamp) => Initial.AddSeconds(unixTimestamp);
    }
}