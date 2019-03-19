using System;
using System.ComponentModel.DataAnnotations;

namespace RM.Common.Utils.Validation
{
    /// <summary>
    /// Provides a set of extenions for validating objects, using their associated <see cref="ValidationAttribute"/> custom attributes.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Throws a <see cref="ValidationException"/> if the given <paramref name="obj"/> is not valid.
        /// </summary>
        /// <remarks>
        /// This method evaluates all <see cref="ValidationAttribute"/>s attached to the object's type.
        /// </remarks>
        /// <param name="obj">The object instance to test.  It cannot be null.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="obj"/> is null.</exception>
        /// <exception cref="ValidationException">When <paramref name="obj"/> is found to be invalid.</exception>
        public static void Validate(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            Validator.ValidateObject(obj, new ValidationContext(obj, null, null), true);
        }

        /// <summary>
        /// Gets a value that indicates whether <paramref name="obj"/> is valid.
        /// </summary>
        /// <param name="obj">The object instance to test.  It cannot be null.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="obj"/> is null.</exception>
        /// <returns>true if <paramref name="obj"/> is valid; otherwise, false.</returns>
        public static bool IsValid(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return Validator.TryValidateObject(obj, new ValidationContext(obj, null, null), null, true);
        }
    }
}
