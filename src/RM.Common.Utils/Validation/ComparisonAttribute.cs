using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Represents a base attribute which compares attribute-target property/field with other property represented by <see cref="OtherPropertyOfField"/>.
    /// NOTE: Works only on <see cref="IComparable"/> properties or fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class ComparisonAttribute : ValidationAttribute
    {
        /// <summary>
        /// The name of the other property or field.
        /// </summary>
        public string OtherPropertyOfField { get; }

        private readonly Func<int, bool> _predicate;

        /// <summary>
        /// Creates a new instance of the <see cref="ComparisonAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyOfField">The name of the other property or field.</param>
        /// <param name="predicate">The predicate to validate the result of comparing (<see cref="IComparable.CompareTo"/>) attribute target and <see cref="OtherPropertyOfField"/>.</param>
        /// <param name="errorMessage">A non-localized error message to use in <see cref="ValidationAttribute.ErrorMessageString"/>.</param>
        protected ComparisonAttribute(string otherPropertyOfField, Func<int, bool> predicate, string errorMessage) : base(errorMessage)
        {
            if (string.IsNullOrWhiteSpace(otherPropertyOfField)) throw new ArgumentException("Value cannot be null, empty or contain only whitespace characters.", nameof(otherPropertyOfField));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            OtherPropertyOfField = otherPropertyOfField;
            _predicate = predicate;
        }

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var thisMemberType = validationContext.ObjectType.GetRuntimeProperty(validationContext.MemberName)?.PropertyType ?? validationContext.ObjectType.GetRuntimeField(validationContext.MemberName).FieldType;
            if (!typeof(IComparable).IsAssignableFrom(thisMemberType)) return new ValidationResult($"The \'{nameof(ComparisonAttribute)}\' can only be attached to the \'{nameof(IComparable)}\' properties/fields.");          
            if (value == null) return ValidationResult.Success;

            var otherProperty = validationContext.ObjectType.GetRuntimeProperty(OtherPropertyOfField);
            var otherField = otherProperty == null ? validationContext.ObjectType.GetRuntimeField(OtherPropertyOfField) : null;
            if (otherProperty == null && otherField == null) return new ValidationResult($"The \'{OtherPropertyOfField}\' not found int the {validationContext.ObjectType} class.");
            
            var otherValue = otherProperty != null ? otherProperty.GetValue(validationContext.ObjectInstance) : otherField.GetValue(validationContext.ObjectInstance);
            if (otherValue == null) return ValidationResult.Success;

            try
            {
                if (value is IComparable thisValue && _predicate(thisValue.CompareTo(otherValue))) return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult($"The \'{OtherPropertyOfField}\' has incompatible type with \'{validationContext.MemberName}\'.");
            }

            return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, ((MemberInfo)otherProperty ?? otherField).GetCustomAttribute<DisplayAttribute>()?.GetName() ?? OtherPropertyOfField));
        }
    }
}