using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns ConcurrentDictionary enumerator over keys.
        /// NOTE: Returns not a keys snapshot, you may get dirty reads.
        /// </summary>
        public static IEnumerable<TKey> Keys<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Select(x => x.Key);
        }

        /// <summary>
        /// Returns ConcurrentDictionary enumerator over values.
        /// NOTE: Returns not a values snapshot, you may get dirty reads.
        /// </summary>
        public static IEnumerable<TValue> Values<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Select(x => x.Value);
        }

        /// <summary>
        /// Attempts to remove and return the value that has a specified <paramref name="key"/>.
        /// </summary>
        public static TValue TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key) where  TValue : class
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dictionary.TryRemove(key, out var obj) ? obj : default(TValue);
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="ConcurrentDictionary{TKey,TValue}"/>.
        /// </summary>
        public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dictionary.TryRemove(key, out _);
        }

        /// <summary>
        /// Tries to read value and returns the value if successfully read. Otherwise return default value for value's type.
        /// </summary>
        public static TValue TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where  TValue : class
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dictionary.TryGetValue(key, out var value) ? value : default(TValue);
        }

        #region AddOrUpdateRange
        /// <summary>
        /// Adds a range of keys/values to the specified <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        public static void AddOrUpdateRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection) source[item.Key] = item.Value;
        }

        /// <summary>
        /// Adds a range of keys/values to the specified <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        public static void AddOrUpdateRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TValue> collection, Func<TValue, TKey> keyFor)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (keyFor == null) throw new ArgumentNullException(nameof(keyFor));

            foreach (var item in collection) source[keyFor(item)] = item;
        }

        /// <summary>
        /// Adds a range of keys/values to the specified <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        public static void AddOrUpdateRange<TSource, TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TSource> collection, Func<TSource, TKey> keyFor, Func<TSource, TValue> valueFor)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (keyFor == null) throw new ArgumentNullException(nameof(keyFor));
            if (valueFor == null) throw new ArgumentNullException(nameof(valueFor));

            foreach (var item in collection) source[keyFor(item)] = valueFor(item);
        }

        #endregion

        #region ToConcurrentDictionary
        /// <summary>Creates a <see cref="ConcurrentDictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" /> according to a specified key selector function.</summary>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="ConcurrentDictionary{TKey,TValue}" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ToConcurrentDictionary(keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>Creates a <see cref="ConcurrentDictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" /> according to a specified key selector function.</summary>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="ConcurrentDictionary{TKey,TValue}" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.ToConcurrentDictionary(keySelector, x => x, comparer);
        }

        /// <summary>Creates a <see cref="ConcurrentDictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" /> according to a specified key selector function.</summary>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="ConcurrentDictionary{TKey,TValue}" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="valueSelector">A function to extract a value from each element.</param>
        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> valueSelector)
        {
            return source.ToConcurrentDictionary(keySelector, valueSelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>Creates a <see cref="ConcurrentDictionary{TKey,TValue}" /> from an <see cref="IEnumerable{T}" /> according to a specified key selector function.</summary>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to create a <see cref="ConcurrentDictionary{TKey,TValue}" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="valueSelector">A function to extract a value from each element.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}" /> to compare keys.</param>
        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> valueSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (valueSelector == null) throw new ArgumentNullException(nameof(valueSelector));

            var dictionary = new ConcurrentDictionary<TKey, TElement>(comparer);
            dictionary.AddOrUpdateRange(source, keySelector, valueSelector);

            return dictionary;
        }
        #endregion
    }
}