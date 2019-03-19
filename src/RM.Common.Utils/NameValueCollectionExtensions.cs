using System;
using System.Collections.Specialized;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="NameValueCollection" />.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets value from <paramref name="collection"/> using the specified <paramref name="predicate"/> (over keys).
        /// </summary>
        /// <param name="collection">The collection to search.</param>
        /// <param name="predicate">The predicate which invoked over <see cref="NameValueCollection.AllKeys"/>.</param>
        /// <returns></returns>
        public static string GetValue(this NameValueCollection collection, Func<string, bool> predicate)
        {
            Ensure.IsNotNull(collection, nameof(collection));
            Ensure.IsNotNull(predicate, nameof(predicate));

            var key = collection.AllKeys.FirstOrDefault(predicate);
            return key != null ? collection[key] : null;
        }
    }
}