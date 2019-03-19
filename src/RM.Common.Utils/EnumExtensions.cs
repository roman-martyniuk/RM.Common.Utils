using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns an indication whether a constant with a specified <paramref name="value"/> exists in an enumeration.
        /// </summary>
        public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.IsDefined(value);
        }

        /// <summary>
        /// Gets the detailed information about a specified enum value.
        /// NOTE: Throws exception if the specified enum <paramref name="value"/> is not defined in the <typeparamref name="TEnum"/> type.
        /// </summary>
        public static Enum<TEnum>.EnumValueContext GetContext<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetContext(value);
        }

        /// <summary>Tries to get the detailed information about a specified enum value.</summary>
        public static Enum<TEnum>.EnumValueContext TryGetContext<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.TryGetContext(value);
        }

        /// <summary>Gets the name of the specified enum <paramref name="value"/>.</summary>
        public static string GetName<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetName(value);
        }

        /// <summary>Gets the localized name (<see cref="DisplayAttribute.GetName()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetDisplayName<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetDisplayName(value);
        }

        /// <summary>
        /// Gets the localized short name (<see cref="DisplayAttribute.GetShortName()"/>) of the specified enum <paramref name="value"/>.
        /// NOTE: If <see cref="DisplayAttribute.ShortName"/> is <c>null</c>, the value from <see cref="DisplayAttribute.GetName()"/> will be returned.
        /// </summary>
        public static string GetShortName<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetShortName(value);
        }

        /// <summary>Gets the localized description (<see cref="DisplayAttribute.GetDescription()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetDescription(value);
        }

        /// <summary>Gets the localized group name (<see cref="DisplayAttribute.GroupName()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetGroupName<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetGroupName(value);
        }

        /// <summary>Gets the localized promt (<see cref="DisplayAttribute.GetPrompt()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetPrompt<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetPromt(value);
        }
        
        /// <summary>
        /// Gets the collection of the custom attributes for the specified enum <paramref name="value"/>.
        /// </summary>
        public static IReadOnlyCollection<Attribute> GetAttributes<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Enum<TEnum>.GetAttributes(value);
        }

        /// <summary>Parses the string representation of the specified enum value.</summary>
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return Enum<TEnum>.Parse(value, ignoreCase);
        }

        /// <summary>Attempts to parse the string representation of the specified enum value.</summary>
        public static TEnum? ToEnumSafe<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return Enum<TEnum>.TryParse(value, ignoreCase);
        }

        /// <summary>
        /// Converts the specified enum <paramref name="value"/> to its <see cref="int"/> representation.
        /// </summary>
        public static int ToInt32<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return value.ToInt32(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the specified enum <paramref name="value"/> to its <see cref="long"/> representation.
        /// </summary>
        public static long ToInt64<TEnum>(this TEnum value) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return value.ToInt64(CultureInfo.InvariantCulture);
        }
    }
}