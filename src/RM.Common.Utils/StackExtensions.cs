using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for <see cref="Stack{T}"/> and <see cref="ConcurrentStack{T}"/>.
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Attempts to poop and return the object at the top of the <see cref="ConcurrentStack{T}"/>
        /// </summary>
        public static T TryPop<T>(this Stack<T> stack) where T : class
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            return stack.Count > 0 ? stack.Pop() : default(T);
        }

        /// <summary>
        /// Attempts to poop and return the object at the top of the <see cref="ConcurrentStack{T}"/>
        /// </summary>
        public static T TryPop<T>(this ConcurrentStack<T> stack) where T : class
        {
            if (stack == null) throw new ArgumentNullException(nameof(stack));

            return stack.TryPop(out var item) ? item : default(T);
        }
    }
}