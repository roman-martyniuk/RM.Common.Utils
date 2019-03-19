using System;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Represents an attribute which validates that attribute-target property/field greater than other property represented by <see cref="ComparisonAttribute.OtherPropertyOfField"/>.
    /// NOTE: Works only on <see cref="IComparable"/> properties or fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GreaterThanAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="GreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyOfField">The name of the other property or field.</param>
        public GreaterThanAttribute(string otherPropertyOfField) : base(otherPropertyOfField, x => x > 0, "'{0}' must be greater than '{1}'.") { }
    }
}