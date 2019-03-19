using System;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly TimeSpan _maxDailyTime = new TimeSpan(0, 23, 59, 59, 999).Add(TimeSpan.FromTicks(9999));

        /// <summary>
        /// Returns a new <see cref="T:System.DateTime" /> that adds the specified number of weeks to the value of this instance.
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="weeks">A number of whole and fractional days. The <paramref name="weeks" /> parameter can be negative or positive.</param>
        /// <returns>An DateTime whose value is the sum of the date and time represented by this instance and the number of weeks represented by <paramref name="weeks" />.</returns>
        public static DateTime AddWeeks(this DateTime value, int weeks) => value.AddDays(weeks * 7);

        /// <summary>
        /// Returns first next occurence of specified DayOfTheWeek.
        /// NOTE: The time portion is set to 0:00:00.000.
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="dayOfWeek">A DayOfWeek to find the next occurence of</param>
        /// <returns>A DateTime whose value is the sum of the date represented by this instance and the enum value represented by the <paramref name="dayOfWeek"/>.</returns>
        public static DateTime Next(this DateTime value, DayOfWeek dayOfWeek)
        {
            var currentDay = (int)value.DayOfWeek;
            var targetDay = (int)dayOfWeek;

            if (targetDay <= currentDay) targetDay += 7;

            return value.Date.AddDays(targetDay - currentDay);
        }

        /// <summary>
        /// Returns previous "first" occurence of specified <paramref name="dayOfWeek"/>.
        /// NOTE: The time portion is set to 0:00:00.000.
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="dayOfWeek">A DayOfWeek to find the previous occurence of</param>
        /// <returns>A DateTime whose value is the sum of the date represented by this instance and the enum value represented by the day.</returns>
        public static DateTime Prev(this DateTime value, DayOfWeek dayOfWeek)
        {
            var currentDay = (int)value.DayOfWeek;
            var targetDay = (int)dayOfWeek;

            if (targetDay >= currentDay) targetDay -= 7;

            return value.Date.AddDays(targetDay - currentDay);
        }

        /// <summary>
        /// Returns true if the day is Saturday or Sunday
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>boolean value indicating if the date is a weekend</returns>
        public static bool IsWeekend(this DateTime value) => value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        /// Returns the absolute end of the given day (the last tick of the last hour for the given date)
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the absolute end of the given day (the last tick of the last hour for the given date)</returns>
        public static DateTime EndOfDay(this DateTime value) => new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999, value.Kind).AddTicks(9999);

        /// <summary>
        /// Get the quarter that the datetime is in.
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns 1 to 4 that represenst the quarter that the datetime is in.</returns>
        public static int Quarter(this DateTime value) => (value.Month - 1) / 3 + 1;

        /// <summary>
        /// Returns the Start of the given month (the fist millisecond of the given date)
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the Start of the given month (the fist millisecond of the given date)</returns>
        public static DateTime BeginningOfMonth(this DateTime value) => new DateTime(value.Year, value.Month, 1, 0, 0, 0, 0, value.Kind);

        /// <summary>
        /// Returns the absolute end of the given month (the last tick of the last hour for the given date)
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the absolute end of the given month (the last tick of the last hour for the given date)</returns>
        public static DateTime EndOfMonth(this DateTime value) => new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month), 23, 59, 59, 999, value.Kind).AddTicks(9999);

        /// <summary>
        /// Returns the Start of the given week (the fist millisecond of the given date)
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the Start of the given week (the fist millisecond of the given date)</returns>
        public static DateTime BeginningOfWeek(this DateTime value) => value.Prev(DayOfWeek.Sunday).AddDays(1);

        /// <summary>
        /// Returns the absolute end of the given week (the last tick of the last hour for the given date)
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the absolute end of the given week (the last tick of the last hour for the given date)</returns>
        public static DateTime EndOfWeek(this DateTime value) => value.Next(DayOfWeek.Monday).AddTicks(-1);

        /// <summary>
        /// Returns true if the day is before the <paramref name="datetime" />
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="datetime">The target <see cref="T:System.DateTime" /> value.</param>
        /// <returns>boolean value indicating if the date is before <paramref name="datetime" /></returns>
        public static bool IsBefore(this DateTime value, DateTime datetime) => value < datetime;

        /// <summary>
        ///     Returns true if the day is after the <paramref name="datetime" />
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="datetime">The target <see cref="T:System.DateTime" /> value.</param>
        /// <returns>boolean value indicating if the date is before <paramref name="datetime" /></returns>
        public static bool IsAfter(this DateTime value, DateTime datetime) => value > datetime;

        /// <summary>
        /// Returns true if the date is between (including range values) the two <see cref="T:System.DateTime" /> values.
        /// </summary>
        /// <param name="value">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="startDate">Start date to check for</param>
        /// <param name="endDate">End date to check for</param>
        /// <returns>boolean value indicating if the date is between (including range values) the two values</returns>
        public static bool IsBetween(this DateTime value, DateTime startDate, DateTime endDate) => startDate <= value && value <= endDate;

        /// <summary>
        /// Returns true if the date is between (including range values) the two <see cref="T:System.DateTime" /> values.
        /// </summary>
        /// <param name="value">DateTime base, from where the calculation will be preformed.</param>
        /// <param name="startDate">Start date to check for</param>
        /// <param name="endDate">End date to check for</param>
        /// <returns>boolean value indicating if the date is between (including range values) the two values</returns>
        public static bool IsBetween(this DateTime? value, DateTime startDate, DateTime endDate)
        {
            if (value == null) return false;

            return startDate <= value && value <= endDate;
        }

        /// <summary>
        /// Returns the total number of weeks in the specified <paramref name="value"/>.
        /// </summary>
        public static double TotalWeeks(this TimeSpan value) => value.TotalDays / 7;

        /// <summary>
        /// Returns true if the day is the last day of month.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>Boolean value indicating if the date is a last day of month.</returns>
        public static bool IsLastDayOfMonth(this DateTime date) => date.Day == DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// Returns an indication whether the year of the specified <paramref name="date"/> is a leap year.
        /// </summary>
        /// <param name="date">The date to check.</param>
        ///<returns>true if <paramref name="date"/> is a leap year date; otherwise, false.</returns>
        public static bool IsLeapYear(this DateTime date) => DateTime.IsLeapYear(date.Year);

        /// <summary>
        /// Returns the next leap year after the specified <paramref name="date"/>.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>The next leap year.</returns>
        public static int NextLeapYear(this DateTime date)
        {
            var year = date.Year + 1;
            while (true)
            {
                if (DateTime.IsLeapYear(year)) return year;
                year++;
            }
        }

        /// <summary>
        /// Sets the <see cref="DateTime.TimeOfDay"/> component of the specified <paramref name="date"/> instance.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="timeOfDay">The time of day. Must be greater than or equal to <see cref="TimeSpan.Zero"/> and lower than or equal to 23:59:59.999.9999.</param>
        /// <returns>The new <see cref="DateTime"/> instance with the specified <paramref name="timeOfDay"/> component.</returns>
        public static DateTime SetTimeOfDay(this DateTime date, TimeSpan timeOfDay)
        {
            Ensure.IsBetween(timeOfDay, TimeSpan.Zero, _maxDailyTime, nameof(timeOfDay));

            return date.Date.Add(timeOfDay);
        }

        /// <summary>
        /// Sets the day component of the specified <paramref name="date"/>.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="day">The day to set. Must be greater than or equal to 1 and lower than or equal to the total number of days in the month.</param>
        /// <returns>The new <see cref="DateTime"/> instance with the specified <paramref name="day"/> component.</returns>
        public static DateTime SetDayOfMonth(this DateTime date, int day)
        {
            Ensure.IsBetween(day, 1, 31, nameof(day));

            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            Ensure.IsLowerThanOrEqualTo(day, daysInMonth, nameof(day));
          
            return day == date.Day ? date : date.AddDays(day - date.Day);
        }

        /// <summary>
        /// Trims the specified <paramref name="datetime"/> to seconds.
        /// </summary>
        public static DateTime TrimToSeconds(this DateTime datetime) => new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Kind);

        /// <summary>
        /// Trims the specified <paramref name="datetime"/> to seconds.
        /// </summary>
        public static DateTime? TrimToSeconds(this DateTime? datetime) => datetime?.TrimToSeconds();

        /// <summary>
        /// Creates a new DateTime object that has the same number of ticks as the specified DateTime, but is designated as Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime ToUtcKind(this DateTime datetime) => datetime.Kind == DateTimeKind.Utc ? datetime : DateTime.SpecifyKind(datetime, DateTimeKind.Utc);

        /// <summary>
        /// Creates a new DateTime object that has the same number of ticks as the specified DateTime, but is designated as local time.
        /// </summary>
        public static DateTime ToLocalKind(this DateTime datetime) => datetime.Kind == DateTimeKind.Local ? datetime : DateTime.SpecifyKind(datetime, DateTimeKind.Local);
    }
}