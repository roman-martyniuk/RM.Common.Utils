using System;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Represents an attribute which validates that attribute-target property/field greater than other property represented by <see cref="ComparisonAttribute.OtherPropertyOfField"/>.
    /// NOTE: Works only on <see cref="IComparable"/> properties or fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GreaterThanOrEqualToAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="GreaterThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyOfField">The name of the other property or field.</param>
        public GreaterThanOrEqualToAttribute(string otherPropertyOfField) : base(otherPropertyOfField, x => x >= 0, "'{0}' must be greater or equal to '{1}'.") { }
    }
}