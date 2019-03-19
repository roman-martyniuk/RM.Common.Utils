using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Specifies the minimum count of <see cref="ICollection"/> items allowed in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MinCountAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum allowable count of the items in the attached <see cref="ICollection"/> property/field.
        /// </summary>
        public int MinCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinCountAttribute"/> class.
        /// </summary>
        /// <param name="minCount">the minimum allowable count of the items in the attached <see cref="ICollection"/> property/field. Value must be greater than zero.</param>
        public MinCountAttribute(int minCount) : base("The min count of items in '{0}' must be greater or equal to '{1}'.")
        {
            if (minCount <= 0) throw new ArgumentOutOfRangeException(nameof(minCount), $"Value must be greater than zero: {minCount}.");

            MinCount = minCount;
        }


        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (!(value is IEnumerable collection)) return new ValidationResult($"The \'{nameof(MinCountAttribute)}\' can only be attached to the \'IEnumerable\' properties/fields.");

            return Count(collection) >= MinCount ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, MinCount));
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