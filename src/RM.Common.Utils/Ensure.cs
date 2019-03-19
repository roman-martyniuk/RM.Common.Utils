using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents methods that can be used to ensure that parameter values meet expected conditions.
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        /// Ensures that the value of a parameter is between a minimum and a maximum value.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsBetween<T>(T value, T min, T max, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not between {min} and {max}: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsEqualTo<T>(T value, T comparand, string paramName) where T : IEquatable<T>
        {
            if (!value.Equals(comparand)) throw new ArgumentException($"Value is not equal to {comparand}: {value}.", paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsEqualTo<T>(T value, object comparand, string paramName)
        {
            if (!value.Equals(comparand)) throw new ArgumentException($"Value is not equal to {comparand}: {value}.", paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsGreaterThan<T>(T value, T comparand, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(comparand) <= 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than {comparand}: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than or equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsGreaterThanOrEqualTo<T>(T value, T comparand, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(comparand) < 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than or equal to {comparand}: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is lower than a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsLowerThan<T>(T value, T comparand, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(comparand) >= 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not lower than {comparand}: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is lower than or equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsLowerThanOrEqualTo<T>(T value, T comparand, string paramName) where T : IComparable<T>
        {
            if (value.CompareTo(comparand) > 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not lower than or equal to {comparand}: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanOrEqualToZero(int value, string paramName)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than or equal to 0: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanOrEqualToZero(long value, string paramName)
        {
            if (value < 0L) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than or equal to 0: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanOrEqualToZero(decimal value, string paramName)
        {
            if (value < 0m) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than or equal to 0: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanOrEqualToZero(TimeSpan value, string paramName)
        {
            if (value < TimeSpan.Zero) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than or equal to zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanZero(int value, string paramName)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanZero(long value, string paramName)
        {
            if (value <= 0L) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanZero(decimal value, string paramName)
        {
            if (value <= 0m) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsGreaterThanZero(TimeSpan value, string paramName)
        {
            if (value <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(paramName, $"Value is not greater than zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is infinite or greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsInfiniteOrGreaterThanOrEqualToZero(TimeSpan value, string paramName)
        {
            if (value < TimeSpan.Zero && value != Timeout.InfiniteTimeSpan) throw new ArgumentOutOfRangeException(paramName, $"Value is not infinite or greater than or equal to zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is infinite or greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsInfiniteOrGreaterThanZero(TimeSpan value, string paramName)
        {
            if (value != Timeout.InfiniteTimeSpan && value <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(paramName, $"Value is not infinite or greater than zero: {value}.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is not null.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNotNull<T>(T value, string paramName) where T : class
        {
            if (value == null) throw new ArgumentNullException(paramName, "Value cannot be null.");
        }

        /// <summary>
        /// Ensures that the value of a parameter is not null or empty.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNotNullOrEmpty(string value, string paramName)
        {
            if (value == null) throw new ArgumentNullException(paramName);
            if (value.Length == 0) throw new ArgumentException("Value cannot be empty.", paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is not null or empty/whitespace.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNotNullOrWhiteSpace(string value, string paramName)
        {
            if (value == null) throw new ArgumentNullException(paramName);
            if (value.Length == 0) throw new ArgumentException("Value cannot be empty.", paramName);

            for (var i = 0; i < value.Length; i++) if (!char.IsWhiteSpace(value[i])) return;

            throw new ArgumentException("Value cannot contain only whitespace characters.", paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNull<T>(T value, string paramName) where T : class
        {
            if (value != null) throw new ArgumentException("Value must be null.", paramName);
        }

        #region IsNullOrGreaterThan
        /// <summary>
        /// Ensures that the value of a parameter is null or greater than a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNullOrGreaterThan<T>(T? value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value != null) IsGreaterThan(value.Value, comparand, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNullOrGreaterThan<T>(T value, T comparand, string paramName) where T : class, IComparable<T>
        {
            if (value != null) IsGreaterThan(value, comparand, paramName);
        }
        #endregion

        #region IsNullOrGreaterThanOrEqualTo
        /// <summary>
        /// Ensures that the value of a parameter is null or greater than or equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNullOrGreaterThanOrEqualTo<T>(T? value, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (value != null) IsGreaterThanOrEqualTo(value.Value, comparand, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than or equal to a comparand.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <typeparam name="T">Type type of the value.</typeparam>
        public static void IsNullOrGreaterThanOrEqualTo<T>(T value, T comparand, string paramName) where T : class, IComparable<T>
        {
            if (value != null) IsGreaterThanOrEqualTo(value, comparand, paramName);
        }
        #endregion

        #region IsNullOrGreaterThanOrEqualToZero
        /// <summary>
        /// Ensures that the value of a parameter is null or greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanOrEqualToZero(int? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanOrEqualToZero(value.Value, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanOrEqualToZero(long? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanOrEqualToZero(value.Value, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than or equal to zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanOrEqualToZero(decimal? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanOrEqualToZero(value.Value, paramName);
        }
        #endregion

        #region IsNullOrGreaterThanZero
        /// <summary>
        /// Ensures that the value of a parameter is null or greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanZero(int? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanZero(value.Value, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanZero(long? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanZero(value.Value, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanZero(decimal? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanZero(value.Value, paramName);
        }

        /// <summary>
        /// Ensures that the value of a parameter is null or greater than zero.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void IsNullOrGreaterThanZero(TimeSpan? value, string paramName)
        {
            if (value.HasValue) IsGreaterThanZero(value.Value, paramName);
        }
        #endregion

        #region That
        /// <summary>
        /// Ensures that an assertion is true.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">The message to use with the exception that is thrown if the assertion is false.</param>
        public static void That(bool assertion, string message)
        {
            if (!assertion) throw new ArgumentException(message);
        }

        /// <summary>
        /// Ensures that an assertion is true.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">The message to use with the exception that is thrown if the assertion is false.</param>
        /// <param name="paramName">The parameter name.</param>
        public static void That(bool assertion, string message, string paramName)
        {
            if (!assertion) throw new ArgumentException(message, paramName);
        }
        #endregion

        #region Collections
        /// <summary>
        /// Ensures that the collection is not null or empty.
        /// </summary>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionIsNotNullOrEmpty<T>(ICollection<T> collection, string paramName)
        {
            IsNotNull(collection, paramName);

            if (collection.Count == 0) throw new ArgumentOutOfRangeException(paramName, "Collection cannot be empty.");
        }

        /// <summary>
        /// Ensures that the all collection items are not null.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the collection items.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionItemsAreNotNull<T>(ICollection<T> collection, string paramName) where T : class
        {
            if (collection == null) return;
            if (collection.Any(x => x == null)) throw new ArgumentException(paramName, $"Items in collection {paramName} cannot be null.");
        }

        /// <summary>
        /// Ensures that the the collection is not null or empty and all collection items are not null.
        /// </summary>
        /// <typeparam name="T">The type of the collection items.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionIsNotNullOrEmptyAndItemsAreNotNull<T>(ICollection<T> collection, string paramName) where T : class
        {
            CollectionIsNotNullOrEmpty(collection, paramName);
            CollectionItemsAreNotNull(collection, paramName);
        }

        /// <summary>
        /// Ensures that the all collection items are greater than a comparand.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the collection items.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionItemsAreGreaterThan<T>(ICollection<T> collection, T comparand, string paramName) where T : struct, IComparable<T>
        {
            if (collection == null) return;
            if (collection.Any(x => x.CompareTo(comparand) <= 0)) throw new ArgumentOutOfRangeException(paramName, $"Items in collection cannot be lower than {comparand}.");
        }

        /// <summary>
        /// Ensures that the the collection is not null or empty and all collection items are greater than a comparand.
        /// </summary>
        /// <typeparam name="T">The type of the collection items.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="comparand">The comparand.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionIsNotNullOrEmptyAndItemsAreGreaterThan<T>(ICollection<T> collection, T comparand, string paramName) where T : struct, IComparable<T>
        {
            CollectionIsNotNullOrEmpty(collection, paramName);
            CollectionItemsAreGreaterThan(collection, comparand, paramName);
        }

        /// <summary>
        /// Ensures that the all collection items are greater than zero.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionItemsAreGreaterThanZero(ICollection<long> collection, string paramName)
        {
            if (collection == null) return;
            if (collection.Any(x => x <= 0)) throw new ArgumentOutOfRangeException(paramName, "Items in collection cannot be lower than zero.");
        }

        /// <summary>
        /// Ensures that the all collection items is greater than zero.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionItemsAreGreaterThanZero(ICollection<int> collection, string paramName)
        {
            if (collection == null) return;
            if (collection.Any(x => x <= 0)) throw new ArgumentOutOfRangeException(paramName, "Items in collection cannot be lower than zero.");
        }

        /// <summary>
        /// Ensures that the the collection is not null or empty and all collection items are greater than zero.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionIsNotNullOrEmptyAndItemsAreGreaterThanZero(ICollection<long> collection, string paramName)
        {
            CollectionIsNotNullOrEmpty(collection, paramName);
            CollectionItemsAreGreaterThanZero(collection, paramName);
        }

        /// <summary>
        /// Ensures that the the collection is not null or empty and all collection items is greater than zero.
        /// NOTE: Does not throw exception if collection is null or empty.
        /// </summary>
        /// <param name="collection">The collection of items.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void CollectionIsNotNullOrEmptyAndItemsAreGreaterThanZero(ICollection<int> collection, string paramName)
        {
            CollectionIsNotNullOrEmpty(collection, paramName);
            CollectionItemsAreGreaterThanZero(collection, paramName);
        }
        #endregion
    }
}