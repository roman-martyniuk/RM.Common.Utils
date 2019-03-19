using System;
using System.Reflection;

namespace RM.Common.Utils
{
    /// <summary>
    /// Represents extension methods for reflection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Checks whether <paramref name="memberInfo"/> has the specified <paramref name="attributeType"/>.
        /// </summary>
        /// <param name="memberInfo">Member to check.</param>
        /// <param name="attributeType">Attribute to check.</param>
        /// <param name="inherit">Specifiies whether to check among inherited attributes.</param>
        public static bool HasAttribute(this MemberInfo memberInfo, Type attributeType, bool inherit = true)
        {
            Ensure.IsNotNull(memberInfo, nameof(memberInfo));
            Ensure.IsNotNull(attributeType, nameof(attributeType));

            //alternative method MemberInfo.IsDefined does not work properly (inherit parameter is ignored for properties and events)
            return Attribute.IsDefined(memberInfo, attributeType, inherit);
        }

        /// <summary>
        /// Checks whether an <paramref name="memberInfo"/> has the specified attribute.
        /// </summary>
        /// <param name="memberInfo">Member to check.</param>
        /// <param name="inherit">Specifiies whether to check among inherited attributes.</param>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            Ensure.IsNotNull(memberInfo, nameof(memberInfo));

            return Attribute.IsDefined(memberInfo, typeof(T), inherit);
        }

        /// <summary>
        /// Checks whether a <paramref name="type"/> has the parameterless constructor.
        /// </summary>
        /// <param name="type">The type to check.</param>
        public static bool HasParameterlessConstructor(this Type type)
        {
            Ensure.IsNotNull(type, nameof(type));

            return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null) != null;
        }

        /// <summary>
        /// Checks whether the specified <paramref name="derivedType"/> is derived from <paramref name="baseType"/>.
        /// </summary>
        /// <param name="derivedType">The derived type.</param>
        /// <param name="baseType">The base type.</param>
        public static bool IsDerivedFrom(this Type derivedType, Type baseType)
        {
            Ensure.IsNotNull(derivedType, nameof(derivedType));
            Ensure.IsNotNull(baseType, nameof(baseType));

            return baseType.IsAssignableFrom(derivedType);
        }

        /// <summary>
        /// Checks whether the specified <paramref name="derivedType"/> is derived from base type.
        /// </summary>
        /// <typeparam name="TBaseType">The base type.</typeparam>
        /// <param name="derivedType">The derived type.</param>
        public static bool IsDerivedFrom<TBaseType>(this Type derivedType) where TBaseType : class
        {
            Ensure.IsNotNull(derivedType, nameof(derivedType));

            return typeof(TBaseType).IsAssignableFrom(derivedType);
        }
    }
}
