using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Used for specifying a string length constraint for <see cref="ICollection{String}"/> items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CollectionItemStringLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the maximum value for the range
        /// </summary>
        public int MaximumLength { get; }

        /// <summary>
        /// Gets the minimum value for the range
        /// </summary>
        public int MinimumLength { get; }

        /// <summary>
        /// Constructor that accepts the maximum length of the string.
        /// </summary>
        /// <param name="maximumLength">The maximum length, inclusive. It may not be negative.</param>
        public CollectionItemStringLengthAttribute(int maximumLength) : this(0, maximumLength) { }

        /// <summary>
        /// Constructor that accepts the maximum length of the string.
        /// </summary>
        /// <param name="minimumLength">The minimum acceptable length of the string.</param>
        /// <param name="maximumLength">The maximum length, inclusive. It may not be negative.</param>
        public CollectionItemStringLengthAttribute(int minimumLength, int maximumLength)
        {
            if (minimumLength < 0) throw new ArgumentOutOfRangeException(nameof(minimumLength));
            if (maximumLength < 0) throw new ArgumentOutOfRangeException(nameof(maximumLength));
            if (minimumLength > maximumLength) throw new ArgumentOutOfRangeException(nameof(minimumLength));

            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is ICollection<string> collection)) return new ValidationResult($"The \'{nameof(CollectionItemStringLengthAttribute)}\' can only be attached to the \'{nameof(ICollection<string>)}\' properties/fields.");

            return collection.All(x => MinimumLength <= x.Length && x.Length <= MaximumLength) ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, MaximumLength, MinimumLength));
        }
    }
}