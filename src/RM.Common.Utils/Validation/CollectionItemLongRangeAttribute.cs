using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Used for specifying a range constraint for <see cref="ICollection{Int64}"/> items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CollectionItemLongRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum value for the range
        /// </summary>
        public long Minimum { get; }

        /// <summary>
        /// Gets the maximum value for the range
        /// </summary>
        public long Maximum { get; }

        /// <summary>
        /// Constructor that takes <see cref="long"/> minimum and maximum values
        /// </summary>
        /// <param name="minimum">The minimum value, inclusive</param>
        /// <param name="maximum">The maximum value, inclusive</param>
        public CollectionItemLongRangeAttribute(long minimum, long maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is ICollection<long> collection)) return new ValidationResult($"The \'{nameof(CollectionItemLongRangeAttribute)}\' can only be attached to the \'{nameof(ICollection<long>)}\' properties/fields.");

            return collection.All(x => Minimum <= x && x <= Maximum) ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, Minimum, Maximum));
        }
    }
}