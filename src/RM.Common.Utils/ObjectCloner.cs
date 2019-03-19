using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RM.Common.Utils
{
    /// <summary>
    /// Supports cloning, which creates a new instance of a class with the same value as an existing instance.
    /// </summary>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a new object that is a full (deep) copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a full (deep) copy of this instance.</returns>
        T Clone();
    }

    /// <summary>
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCloner
    {
        /// <summary>
        /// Perform a Deep Copy of the object.
        /// <c>NOTE: class must be marked with <see cref="SerializableAttribute"/> attribute.</c>
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source) where T : class
        {
            if (!typeof(T).IsSerializable) throw new ArgumentException("The type must be serializable.", nameof(source));

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default(T);

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
