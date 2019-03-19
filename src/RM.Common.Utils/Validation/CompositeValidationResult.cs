using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Represents a composit validation result.
    /// </summary>
    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        /// <summary>
        /// The validation results.
        /// </summary>
        public IEnumerable<ValidationResult> Results => _results;

        /// <summary>
        /// Creates a new instance of the <see cref="CompositeValidationResult"/> class by using an error message.
        /// </summary>
        /// <param name="errorMessage">The user-visible error message.  If null, <see cref="ValidationAttribute.GetValidationResult"/> will use <see cref="ValidationAttribute.FormatErrorMessage"/> for its error message.</param>
        public CompositeValidationResult(string errorMessage) : base(errorMessage) { }

        /// <summary>
        /// Constructor that accepts an error message as well as a list of member names involved in the validation.
        /// This error message would override any error message provided on the <see cref="ValidationAttribute"/>.
        /// </summary>
        /// <param name="errorMessage">The user-visible error message.  If null, <see cref="ValidationAttribute.GetValidationResult"/> will use <see cref="ValidationAttribute.FormatErrorMessage"/> for its error message.</param>
        /// <param name="memberNames">The list of member names affected by this result.  This list of member names is meant to be used by presentation layers to indicate which fields are in error.</param>
        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }

        /// <inheritdoc />
        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

        /// <summary>
        /// Add validation result to the list of results (<see cref="Results"/>).
        /// </summary>
        /// <param name="validationResult">The result to add.</param>
        public void AddResult(ValidationResult validationResult) => _results.Add(validationResult);
    }
}