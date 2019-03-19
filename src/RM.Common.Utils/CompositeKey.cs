using System;
using System.Collections.Generic;

namespace RM.Common.Utils
{
    /// <summary>
    /// Provides static methods for creating <see cref="CompositeKey{T1,T2}"/> and <see cref="CompositeKey{T1,T2,T3}"/> objects.
    /// </summary>
    public static class CompositeKey
    {
        /// <summary>Creates a new 2-component composite key.</summary>
        /// <param name="key1">The value of the first component of the key.</param>
        /// <param name="key2">The value of the second component of the key.</param>
        public static CompositeKey<T1, T2> Create<T1, T2>(T1 key1, T2 key2) where T1 : IEquatable<T1> where T2 : IEquatable<T2>
        {
            return new CompositeKey<T1, T2>(key1, key2);
        }

        /// <summary>Creates a new 2-component composite key.</summary>
        /// <param name="key1">The value of the first component of the key.</param>
        /// <param name="key2">The value of the second component of the key.</param>
        /// <param name="key3">The value of the third component of the key.</param>
        public static CompositeKey<T1, T2, T3> Create<T1, T2, T3>(T1 key1, T2 key2, T3 key3) where T1 : IEquatable<T1> where T2 : IEquatable<T2> where T3 : IEquatable<T3>
        {
            return new CompositeKey<T1, T2, T3>(key1, key2, key3);
        }
    }

    /// <summary>
    /// Represents a composite key that can be used in <see cref="IDictionary{TKey,TValue}"/> or <see cref="ISet{T}"/>.
    /// </summary>
    public struct CompositeKey<T1, T2> : IEquatable<CompositeKey<T1, T2>>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
    {
        /// <summary>
        /// The first component of the key.
        /// </summary>
        public T1 Key1 { get; }

        /// <summary>
        /// The second component of the key.
        /// </summary>
        public T2 Key2 { get; }

        /// <summary>
        /// Creates new instance of the <see cref="CompositeKey{T1,T2}"/> structure.
        /// </summary>
        /// <param name="key1">The first component of the key.</param>
        /// <param name="key2">The second component of the key.</param>
        public CompositeKey(T1 key1, T2 key2) : this()
        {
            Key1 = key1;
            Key2 = key2;
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(CompositeKey<T1, T2> other)
        {
            return EqualityComparer<T1>.Default.Equals(Key1, other.Key1) && EqualityComparer<T2>.Default.Equals(Key2, other.Key2);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if <paramref name="obj">obj</paramref> and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return obj is CompositeKey<T1, T2> key && Equals(key);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T1>.Default.GetHashCode(Key1) * 397) ^ EqualityComparer<T2>.Default.GetHashCode(Key2);
            }
        }

        /// <summary>
        /// The '==' operator implementation.
        /// </summary>
        public static bool operator ==(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right) => left.Equals(right);

        /// <summary>
        /// The '!=' operator implementation.
        /// </summary>
        public static bool operator !=(CompositeKey<T1, T2> left, CompositeKey<T1, T2> right) => !left.Equals(right);
    }

    /// <summary>
    /// Represents a composite key that can be used in <see cref="IDictionary{TKey,TValue}"/> or <see cref="HashSet{T}"/>.
    /// </summary>
    public struct CompositeKey<T1, T2, T3> : IEquatable<CompositeKey<T1, T2, T3>>
        where T1 : IEquatable<T1>
        where T2 : IEquatable<T2>
        where T3 : IEquatable<T3>
    {
        /// <summary>
        /// The first component of the key.
        /// </summary>
        public T1 Key1 { get; }

        /// <summary>
        /// The second component of the key.
        /// </summary>
        public T2 Key2 { get; }

        /// <summary>
        /// The third component of the key.
        /// </summary>
        public T3 Key3 { get; }

        /// <summary>
        /// Creates new instance of the <see cref="CompositeKey{T1,T2}"/> structure.
        /// </summary>
        /// <param name="key1">The first component of the key.</param>
        /// <param name="key2">The second component of the key.</param>
        /// <param name="key3">The third component of the key.</param>
        public CompositeKey(T1 key1, T2 key2, T3 key3)
            : this()
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if <paramref name="obj">obj</paramref> and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            return obj is CompositeKey<T1, T2, T3> key && Equals(key);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(CompositeKey<T1, T2, T3> other)
        {
            return EqualityComparer<T1>.Default.Equals(Key1, other.Key1) && EqualityComparer<T2>.Default.Equals(Key2, other.Key2) && EqualityComparer<T3>.Default.Equals(Key3, other.Key3);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T1>.Default.GetHashCode(Key1);
                hashCode = (hashCode * 397) ^ EqualityComparer<T2>.Default.GetHashCode(Key2);
                hashCode = (hashCode * 397) ^ EqualityComparer<T3>.Default.GetHashCode(Key3);
                return hashCode;
            }
        }

        /// <summary>
        /// The '==' operator implementation.
        /// </summary>
        public static bool operator ==(CompositeKey<T1, T2, T3> left, CompositeKey<T1, T2, T3> right) => left.Equals(right);

        /// <summary>
        /// The '!=' operator implementation.
        /// </summary>
        public static bool operator !=(CompositeKey<T1, T2, T3> left, CompositeKey<T1, T2, T3> right) => !left.Equals(right);
    }
}