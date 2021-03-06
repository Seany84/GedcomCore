using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Utilities
{
    /// <summary>
    /// contains several useful functions for dealing with enums
    /// </summary>
    static class EnumTagUtil
    {
        /// <summary>
        /// returns all tags that are defined for the given enum type with their corresponding fields
        /// </summary>
        /// <param name="enumType">an enum type</param>
        /// <returns>all tags defined for the given enum type with their corresponding fields</returns>
        public static IDictionary<string, FieldInfo> GetMembers(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("the given type must be an enum");
            }

            var members = new Dictionary<string,FieldInfo>();

            foreach (var field in enumType.GetFields())
            {
                var tags = (EnumTagAttribute[])(field.GetCustomAttributes(typeof(EnumTagAttribute), false));

                if (tags.Length == 0)
                {
                    continue;
                }

                var tag = tags[0];

                members.Add(tag.TagName, field);
            }

            return members;
        }

        /// <summary>
        /// returns the unknown member, the type's default value.
        /// 
        /// This member is annotated by GeneaGedcom.Meta.UnknownEnumAttribute
        /// </summary>
        /// <param name="enumType">an enum type</param>
        /// <param name="object">a object of the given type</param>
        /// <returns>the unknown member</returns>
        public static ValueType GetUnknownMember(Type enumType, object @object)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("the given type must be an enum");
            }

            foreach (var field in enumType.GetFields())
            {
                var attributes = field.GetCustomAttributes(typeof(UnknownEnumAttribute), true) as UnknownEnumAttribute[];

                if (attributes?.Length > 1)
                {
                    throw new InvalidOperationException("attribute appield multiple times");
                }

                if (attributes?.Length == 1)
                {
                    return field.GetValue(@object) as ValueType;
                }
            }

            throw new InvalidOperationException("no suitable tag found");
        }

        /// <summary>
        /// indicates if the enum of the given type has a member with the given tag name
        /// </summary>
        /// <param name="enumType">an enum type</param>
        /// <param name="tag">a tag name</param>
        /// <param name="object">a object of the given type</param>
        /// <returns>true, if the given enum type has a member with the given tag name, or false otherwise</returns>
        public static bool HasMember(Type enumType, string tag, object @object)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("the given type must be an enum");
            }

            return GetMembers(enumType).Any(member => string.Compare(tag, member.Key, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// returns the field of the given enum type that has the given tag name.
        /// 
        /// throws
        /// - ArgumentException if the given type is not an enum
        /// - InvalidOperationException if no suitable field is found
        /// </summary>
        /// <param name="enumType">an enum type</param>
        /// <param name="tag">a tag name</param>
        /// <param name="object">an object of the given type</param>
        /// <returns>the field with the given tag name</returns>
        public static ValueType SelectMember(Type enumType, string tag, object @object)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("the given type must be an enum");
            }

            foreach (var member in GetMembers(enumType))
            {
                if (string.Compare(tag, member.Key, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return member.Value.GetValue(@object) as ValueType;
                }
            }

            throw new InvalidOperationException("no suitable tag found");
        }

        /// <summary>
        /// returns the field of the given enum type that has the given tag name.
        /// 
        /// If no suitable field is found, the given default value is returned
        /// 
        /// throws
        /// - ArgumentException if the given type is not an enum
        /// </summary>
        /// <param name="enumType">an enum type</param>
        /// <param name="tag">a tag name</param>
        /// <param name="object">an object of the given type</param>
        /// <param name="default">a default value that is return if no suitable field is found</param>
        /// <returns>the field with the given tag name</returns>
        public static ValueType SelectMember(Type enumType, string tag, object @object, ValueType @default)
        {
            try
            {
                return SelectMember(enumType, tag, @object);
            }
            catch(InvalidOperationException)
            {
                return @default;
            }
        }

        /// <summary>
        /// returns the first tag name of the given field
        /// </summary>
        /// <param name="valueType">An enum's member</param>
        /// <returns>the first tag name or an empty string, if it has no tags</returns>
        public static string GetFirstTagName(ValueType valueType)
        {
            foreach(var member in GetMembers(valueType.GetType()))
            {
                if (member.Value.GetValue(valueType).Equals(valueType))
                {
                    return member.Key;
                }
            }

            return "";
        }
    }
}
