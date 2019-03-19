using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Provides validation for the complex objects properties/fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidateObjectCollectionAttribute : ValidationAttribute
    {
        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var collection = value as IEnumerable;
            if (collection == null) return new ValidationResult($"The \'{nameof(ValidateObjectCollectionAttribute)}\' can only be attached to the \'IEnumerable\' properties/fields.");

            var results = new List<ValidationResult>();
            foreach (var item in collection)
                Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

            if (results.Count == 0) return ValidationResult.Success;

            var compositeResults = new CompositeValidationResult($"Validation for {validationContext.DisplayName} failed!");
            results.ForEach(compositeResults.AddResult);

            return compositeResults;
        }
    }
}