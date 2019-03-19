using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RM.Common.Utils
{
    /// <summary>
    /// Helper class for enum types.
    /// </summary>
    /// <typeparam name="TEnum">Must be enum type (declared using the <c>enum</c> keyword)</typeparam>
    public static class Enum<TEnum> where TEnum : struct, IConvertible, IComparable, IFormattable
    {
        private static readonly Type _type = typeof(TEnum);

        private static readonly IReadOnlyDictionary<TEnum, EnumValueContext> _dictionary = new ReadOnlyDictionary<TEnum, EnumValueContext>(((TEnum[])Enum.GetValues(_type)).ToDictionary(x => x, x => new EnumValueContext(x)));

        static Enum()
        {
            if (!_type.IsEnum) throw new ArgumentException("Generic Enum type works only with enums");
            //if (_type.GetCustomAttributes<FlagsAttribute>().Any()) throw new ArgumentException("Enums marked with FlagsAttribute is not supported");
        }

        /// <summary>
        /// Represents a readonly collection of plain enum values.
        /// </summary>
        public static IReadOnlyCollection<TEnum> Values => (IReadOnlyCollection<TEnum>)_dictionary.Keys;

        /// <summary>
        /// Represents a readonly collection of enum value's contexts which contains an detailed (value, name, custom attributes) information about enum values.
        /// </summary>
        public static IReadOnlyCollection<EnumValueContext> Contexts => (IReadOnlyCollection<EnumValueContext>)_dictionary.Values;

        /// <summary>
        /// Returns an indication whether a constant with a specified <paramref name="value"/> exists in an enumeration.
        /// </summary>
        public static bool IsDefined(TEnum value) => _dictionary.ContainsKey(value);

        /// <summary>
        /// Gets the detailed information about a specified enum value.
        /// NOTE: Throws exception if the specified enum <paramref name="value"/> is not defined in the <typeparamref name="TEnum"/> type.
        /// </summary>
        public static EnumValueContext GetContext(TEnum value)
        {
            var context = _dictionary.TryGetValue(value);
            if (context == null) throw new ArgumentException($"The specified enum value \"{value}\" is not defined in the \"{_type}\" type.", nameof(value));
            
            return context;
        }

        /// <summary>Tries to get the detailed information about a specified enum value.</summary>
        public static EnumValueContext TryGetContext(TEnum value) => _dictionary.TryGetValue(value);

        /// <summary>Gets the name of the specified enum <paramref name="value"/>.</summary>
        public static string GetName(TEnum value) => GetContext(value).Name;

        /// <summary>Gets the localized name (<see cref="DisplayAttribute.GetName()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetDisplayName(TEnum value) => GetContext(value).DisplayName;
     
        /// <summary>
        /// Gets the localized short name (<see cref="DisplayAttribute.GetShortName()"/>) of the specified enum <paramref name="value"/>.
        /// NOTE: If <see cref="DisplayAttribute.ShortName"/> is <c>null</c>, the value from <see cref="GetDisplayName"/> will be returned.
        /// </summary>
        public static string GetShortName(TEnum value) => GetContext(value).ShortName;

        /// <summary>Gets the localized description (<see cref="DisplayAttribute.GetDescription()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetDescription(TEnum value) => GetContext(value).Description;

        /// <summary>Gets the localized group name (<see cref="DisplayAttribute.GroupName()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetGroupName(TEnum value) => GetContext(value).GroupName;

        /// <summary>Gets the localized promt (<see cref="DisplayAttribute.GetPrompt()"/>) of the specified enum <paramref name="value"/>.</summary>
        public static string GetPromt(TEnum value) => GetContext(value).Prompt;
       
        /// <summary>
        /// Gets the collection of the custom attributes for the specified enum <paramref name="value"/>.
        /// </summary>
        public static IReadOnlyCollection<Attribute> GetAttributes(TEnum value) => GetContext(value).Attributes;

        /// <summary>
        /// Gets the custom attribute of the specified enum <paramref name="value"/>.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(TEnum value) where TAttribute : Attribute => GetContext(value).GetAttribute<TAttribute>();

        /// <summary>
        /// Parses the string representation of the specified enum value.
        /// </summary>
        public static TEnum Parse(string value, bool ignoreCase = true)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return (TEnum)Enum.Parse(_type, value, ignoreCase);
        }

        /// <summary>
        /// Attempts to parse the string representation of the specified enum value.
        /// </summary>
        public static TEnum? TryParse(string value, bool ignoreCase = true)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            return Enum.TryParse(value, ignoreCase, out TEnum result) ? (TEnum?)result : null;
        }

        /// <summary>
        /// Finds the specified enum value using predicate.
        /// </summary>
        public static TEnum Find(Func<EnumValueContext, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return _dictionary.Values.First(predicate).Value;
        }

        /// <summary>
        /// Tries to find the specified enum value using predicate.
        /// </summary>
        public static TEnum? TryFind(Func<EnumValueContext, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var context = _dictionary.Values.FirstOrDefault(predicate);

            return context?.Value;
        }

        /// <summary>
        /// Represents a detailed information about the enum value.
        /// </summary>
        public sealed class EnumValueContext
        {
            private readonly DisplayAttribute _displayAttribute;

            internal EnumValueContext(TEnum value)
            {
                Value = value;
                Name = Enum.GetName(_type, Value);

                var memberInfo = _type.GetMember(Name)[0];
                Attributes = memberInfo.GetCustomAttributes().ToList().AsReadOnly();

                _displayAttribute = Attributes.FirstOfTypeOrDefault<DisplayAttribute>();
            }

            /// <summary>Gets the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public TEnum Value { get; }

            /// <summary>Gets the name of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public string Name { get; }
            
            /// <summary>Gets the localized name (<see cref="DisplayAttribute.GetName()"/>) of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public string DisplayName => _displayAttribute?.GetName();

            /// <summary>
            /// Gets the localized short name (<see cref="DisplayAttribute.GetShortName()"/>) of the enum value represented by current <see cref="EnumValueContext"/>.
            /// NOTE: If <see cref="DisplayAttribute.ShortName"/> is <c>null</c>, the value from <see cref="DisplayName"/> will be returned.
            /// </summary>
            public string ShortName => _displayAttribute?.GetShortName();

            /// <summary>Gets the localized description (<see cref="DisplayAttribute.GetDescription()"/>) of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public string Description => _displayAttribute?.GetDescription();

            /// <summary>Gets the localized group name (<see cref="DisplayAttribute.GetGroupName()"/>) of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public string GroupName => _displayAttribute?.GetGroupName();

            /// <summary>Gets the localized promt (<see cref="DisplayAttribute.GetPrompt()"/>) of the specified enum value.</summary>
            public string Prompt => _displayAttribute?.GetPrompt();

            /// <summary>Gets the custom attributes of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public IReadOnlyCollection<Attribute> Attributes { get; }

            /// <summary>Gets the custom attribute of the enum value represented by current <see cref="EnumValueContext"/>.</summary>
            public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute => Attributes.FirstOfTypeOrDefault<TAttribute>();
        }
    }
}