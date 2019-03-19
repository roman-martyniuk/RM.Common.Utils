using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> extensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region IsEmpty, IsNullOrEmpty, IsNotNullOrEmpty
        /// <summary>
        /// Gets a value indicating whether the current collection is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is empty. </returns>
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return !source.Any();
        }

        /// <summary>
        /// Gets a value indicating whether the current collection is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is empty. </returns>
        public static bool IsEmpty<TSource>(this ICollection<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Count == 0;
        }

        /// <summary>
        /// Gets a value indicating whether the current collection is not empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is not empty. </returns>
        public static bool IsNotEmpty<TSource>(this ICollection<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Count > 0;
        }

        /// <summary>
        /// Gets a value indicating whether the current collection is not empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is not empty. </returns>
        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Any();
        }

        /// <summary>
        /// Gets a value indicating whether the current collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is null or empty. </returns>
        public static bool IsNullOrEmpty<TSource>(this ICollection<TSource> source) => source == null || source.Count == 0;

        /// <summary>
        /// Gets a value indicating whether the current collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is null or empty. </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source) => source == null || !source.Any();

        /// <summary>
        /// Gets a value indicating whether the current collection is not null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is not null or empty. </returns>
        public static bool IsNotNullOrEmpty<TSource>(this ICollection<TSource> source) => source != null && source.Count > 0;

        /// <summary>
        /// Gets a value indicating whether the current collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>A value indicating whether the collection is null or empty. </returns>
        public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source) => source != null && source.Any();

        #endregion

        /// <summary>
        /// Returns first element of an IEnumerable based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>First element of an IEnumerable based on a specified type.</returns>
        public static TResult FirstOfType<TResult>(this IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.OfType<TResult>().First();
        }

        /// <summary>
        /// Returns first element of an IEnumerable based on a specified type or default value (if element not found).
        /// </summary>
        /// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <returns>First element of an IEnumerable based on a specified type or default value (if element not found).</returns>
        public static TResult FirstOfTypeOrDefault<TResult>(this IEnumerable source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.OfType<TResult>().FirstOrDefault();
        }

        /// <summary>
        /// Performs the specified action on each element of the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An element sequence.</param>
        /// <param name="action">The delegate to perform on each element of the sequence.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source) action(item);
        }

        #region EqualsTo
        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <param name="first">An sequence to compare to <paramref name="second"/>. Can be null.</param>
        /// <param name="second">An sequence to compare to <paramref name="first"/>. Can be null.</param>
        /// <returns><c>true</c> if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, <c>false</c>.</returns>
        public static bool EqualsTo<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) => EqualsTo(first, second, x => x, null);

        /// <summary>
        /// Determines whether two sequences are equal by comparing their elements by using a specified <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <param name="first">An sequence to compare to <paramref name="second"/>. Can be null.</param>
        /// <param name="second">An sequence to compare to <paramref name="first"/>. Can be null.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to use to compare elements. Can be null.</param>
        /// <returns><c>true</c> if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, <c>false</c>.</returns>
        public static bool EqualsTo<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) => EqualsTo(first, second, x => x, comparer);

        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="orderByKeySelector"/>.</typeparam>
        /// <param name="first">An sequence to compare to <paramref name="second"/>. Can be null.</param>
        /// <param name="second">An sequence to compare to <paramref name="first"/>. Can be null.</param>
        /// <param name="orderByKeySelector">A function to extract a key from an element. Can not be null.</param>
        /// <returns><c>true</c> if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, <c>false</c>.</returns>
        public static bool EqualsTo<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> orderByKeySelector) => EqualsTo(first, second, orderByKeySelector, null);

        /// <summary>
        /// Determines whether two sequences are equal by comparing their elements by using a specified <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="orderByKeySelector"/>.</typeparam>
        /// <param name="first">An sequence to compare to <paramref name="second"/>. Can be null.</param>
        /// <param name="second">An sequence to compare to <paramref name="first"/>. Can be null.</param>
        /// <param name="orderByKeySelector">A function to extract a key from an element. Can not be null.</param>
        /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to use to compare elements. Can be null.</param>
        /// <returns><c>true</c> if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, <c>false</c>.</returns>
        public static bool EqualsTo<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> orderByKeySelector, IEqualityComparer<TSource> comparer)
        {
            if (orderByKeySelector == null) throw new ArgumentNullException(nameof(orderByKeySelector));

            if (first == null && second == null) return true;
            if (first == null) return second.IsEmpty();
            if (second == null) return first.IsEmpty();

            return first.OrderBy(orderByKeySelector).SequenceEqual(second.OrderBy(orderByKeySelector), comparer);
        }
        #endregion

        /// <summary>
        /// Returns the <paramref name="source"/> without <paramref name="item"/> using default equality comparer to compare values.
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T item)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Except(Enumerable.Repeat(item, 1));
        }

        /// <summary>
        /// Converts the <paramref name="source"/> collection to the <see cref="HashSet{T}"/> using the default equality comparer..
        /// </summary>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return new HashSet<T>(source);
        }

        /// <summary>
        /// Converts the <paramref name="source"/> collection to the <see cref="HashSet{T}"/> using the specified <paramref name="comparer"/>.
        /// </summary>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            return new HashSet<T>(source, comparer);
        }

        /// <summary>
        /// Determines whether two sequences are equal.
        /// </summary>
        public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> equal)
        {
            if (equal == null) throw new ArgumentNullException(nameof(equal));

            if (first == null && second == null) return true;
            if (first == null) return second.IsEmpty();
            if (second == null) return first.IsEmpty();

            using (var enumerator1 = first.GetEnumerator())
            using (var enumerator2 = second.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    if (!enumerator2.MoveNext() || !equal(enumerator1.Current, enumerator2.Current)) return false;
                }
                if (enumerator2.MoveNext()) return false;
            }
            return true;
        }
    }
}