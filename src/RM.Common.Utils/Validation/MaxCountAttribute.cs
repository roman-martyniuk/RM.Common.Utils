using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Specifies the maximum count of <see cref="ICollection"/> items allowed in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MaxCountAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the maximum allowable count of the items in the attached <see cref="ICollection"/> property/field.
        /// </summary>
        public int MaxCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxCountAttribute"/> class.
        /// </summary>
        /// <param name="maxCount">the maximum allowable count of the items in the attached <see cref="ICollection"/> property/field. Value must be greater than zero.</param>
        public MaxCountAttribute(int maxCount) : base("The max count of items in '{0}' must be less or equal to '{1}'.")
        {
            if (maxCount <= 0) throw new ArgumentOutOfRangeException(nameof(maxCount), $"Value must be greater than zero: {maxCount}.");

            MaxCount = maxCount;
        }

        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is IEnumerable collection)) return new ValidationResult("The \'MaxCountAttribute\' can only be attached to the \'IEnumerable\' properties/fields.");

            return Count(collection) <= MaxCount ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, MaxCount));
        }

        private static int Count(IEnumerable items)
        {
            var enumerator = items.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext()) count++;
            return count;
        }
    }
}