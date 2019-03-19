using System;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Represents an attribute which validates that attribute-target property/field less than or equal to other property/field represented by <see cref="ComparisonAttribute.OtherPropertyOfField"/>.
    /// NOTE: Works only on <see cref="IComparable"/> properties or fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class LessThanOrEqualToAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="LessThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyOfField">The name of the other property or field.</param>
        public LessThanOrEqualToAttribute(string otherPropertyOfField) : base(otherPropertyOfField, x => x <= 0, "'{0}' must be less or equal to '{1}'.") { }
    }
}