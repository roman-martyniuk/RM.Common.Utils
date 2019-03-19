using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Used for specifying a range constraint for <see cref="ICollection{Int32}"/> items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CollectionItemIntRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum value for the range
        /// </summary>
        public int Minimum { get; }

        /// <summary>
        /// Gets the maximum value for the range
        /// </summary>
        public int Maximum { get; }

        /// <summary>
        /// Constructor that takes <see cref="int"/> minimum and maximum values
        /// </summary>
        /// <param name="minimum">The minimum value, inclusive</param>
        /// <param name="maximum">The maximum value, inclusive</param>
        public CollectionItemIntRangeAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is ICollection<int> collection)) return new ValidationResult($"The \'{nameof(CollectionItemIntRangeAttribute)}\' can only be attached to the \'{nameof(ICollection<int>)}\' properties/fields.");

            return collection.All(x => Minimum <= x && x <= Maximum) ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, Minimum, Maximum));
        }
    }
}