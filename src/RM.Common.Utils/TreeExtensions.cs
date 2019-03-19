using System;
using System.Collections.Generic;
using System.Linq;

namespace RM.Common.Utils
{
    /// <summary>
    /// Extensions for tree-like object structures.
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// Returns a collection of elements that contains all ancestors of current node.
        /// </summary>
        public static IEnumerable<T> Ancestors<T>(this T node, Func<T, T> parentOf)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (parentOf == null) throw new ArgumentNullException(nameof(parentOf));

            return AncestorsCore(node, parentOf);
        }

        private static IEnumerable<T> AncestorsCore<T>(T node, Func<T, T> parentOf)
        {
            var parent = parentOf(node);
            while (parent != null)
            {
                yield return parent;
                parent = parentOf(parent);
            }
        }

        /// <summary>
        /// Returns a collection of elements that contains current node with all ancestors.
        /// </summary>
        public static IEnumerable<T> AncestorsAndSelf<T>(this T node, Func<T, T> ancestorSelector)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (ancestorSelector == null) throw new ArgumentNullException(nameof(ancestorSelector));

            return node.AsEnumerable().Concat(node.Ancestors(ancestorSelector));
        }

        /// <summary>
        /// Returns a collection of the descendant elements for this element.
        /// </summary>
        public static IEnumerable<T> Descendants<T>(this T node, Func<T, IEnumerable<T>> childrenOf)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (childrenOf == null) throw new ArgumentNullException(nameof(childrenOf));

            return DescendantsCore(node, childrenOf);
        }

        private static IEnumerable<T> DescendantsCore<T>(T node, Func<T, IEnumerable<T>> childrenOf)
        {
            foreach (var child in childrenOf(node))
            {
                yield return child;

                foreach (var descendant in child.Descendants(childrenOf)) yield return descendant;
            }
        }

        /// <summary>
        /// Returns a collection of elements that contains current node with all descendant nodes for this node.
        /// </summary>
        public static IEnumerable<T> DescendantsAndSelf<T>(this T node, Func<T, IEnumerable<T>> childrenOf)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (childrenOf == null) throw new ArgumentNullException(nameof(childrenOf));

            return node.AsEnumerable().Concat(node.Descendants(childrenOf));
        }

        private static IEnumerable<T> AsEnumerable<T>(this T node) => Enumerable.Repeat(node, 1);
    }
}