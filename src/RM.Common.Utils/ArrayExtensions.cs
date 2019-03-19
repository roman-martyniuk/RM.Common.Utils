using System;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for arrays.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Combines 2 arrays together.
        /// </summary>
        /// <param name="first">The first array.</param>
        /// <param name="second">The second array.</param>
        public static T[] Combine<T>(this T[] first, T[] second)
        {
            var result = new T[first.Length + second.Length];
            
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);

            return result;
        }

        /// <summary>
        /// Combines 3 arrays together.
        /// </summary>
        /// <param name="first">The first array.</param>
        /// <param name="second">The second array.</param>
        /// <param name="third">The third array.</param>
        public static T[] Combine<T>(this T[] first, T[] second, T[] third)
        {
            var result = new T[first.Length + second.Length + third.Length];

            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            Buffer.BlockCopy(third, 0, result, first.Length + second.Length, third.Length);

            return result;
        }
    }
}