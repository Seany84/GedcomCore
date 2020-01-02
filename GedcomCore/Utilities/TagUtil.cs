using System;
using System.Collections.Generic;
using System.Reflection;
using GedcomCore.Framework.Entities;
using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Utilities
{
    /// <summary>
    /// contains several useful functions for dealing with tags
    /// </summary>
    internal static class TagUtil
    {
        /// <summary>
        /// returns the property of the given object, that holds objects for the given tag name
        /// 
        /// throws
        /// - InternalException if more than one suitable properties are found
        /// - MemberNotFoundException if no suitable property is found
        /// </summary>
        /// <param name="object">object that is considered</param>
        /// <param name="tagName">tag name of the searched property</param>
        /// <param name="mustBeReadable">indicates if the found property must be readable in order to be considered</param>
        /// <param name="mustBeWriteable">indicates if the found property must be writeable in order to be considered</param>
        /// <returns>one of Object's proerties that matches the given criteria</returns>
        public static PropertyInfo GetMember(object @object, string tagName, bool mustBeReadable, bool mustBeWriteable)
        {
            var members = new List<PropertyInfo>();

            foreach (var member in @object.GetType().GetProperties())
            {
                foreach (var attrib in ((member.GetCustomAttributes(typeof(TagAttribute), false)) as TagAttribute[]))
                {
                    if (attrib.TagName == tagName)
                    {
                        if (mustBeReadable && !member.CanRead)
                        {
                            continue;
                        }

                        if (mustBeWriteable && !member.CanWrite)
                        {
                            continue;
                        }

                        members.Add(member);
                        continue;
                    }
                }
            }

            if (members.Count > 1)
            {
                throw new InternalException("more than one properties are tagged with " + tagName);
            }
            if (members.Count == 0)
            {
                throw new MemberNotFoundException("no property is tagged with " + tagName, tagName);
            }
            return members[0];
        }

        /// <summary>
        /// returns the type for the given property and tag name
        /// </summary>
        /// <param name="property">property for which the type shall be determined</param>
        /// <param name="tagName">one of the tag names for the given properties</param>
        /// <returns>the type for the given property and tag name</returns>
        public static Type GetLineType(PropertyInfo property, string tagName)
        {
            foreach(var attrib in ((property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[]))
            {
                if (String.Compare(attrib.TagName, tagName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (attrib.Type != null)
                    {
                        return attrib.Type;
                    }
                }
            }

            return property.PropertyType;
        }

        /// <summary>
        /// returns an enumeration with all tagged properties of the given type with their type-tag combinations
        /// </summary>
        /// <param name="type">a type</param>
        /// <returns>all tagged properties of the given type with their type-tag combinations</returns>
        public static IEnumerable<KeyValuePair<PropertyInfo, IDictionary<Type, string>>> GetTags(Type type)
        {
            foreach(var prop in type.GetProperties())
            {
                var types = new Dictionary<Type, string>();

                foreach (var attrib in (prop.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[])
                {
                    var t = attrib.Type == null ? prop.PropertyType : attrib.Type;
                    if (types.ContainsKey(t))
                    {
                        types[t] = null;
                    }
                    else
                    {
                        types.Add(t, attrib.TagName);
                    }
                }

                if (types.Count > 0)
                {
                    yield return new KeyValuePair<PropertyInfo, IDictionary<Type, string>>(prop, types);
                }
            }
        }

        public static string GetTagName(PropertyInfo property)
        {
            foreach (var attrib in (property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[])
            {
                return attrib.TagName;
            }

            return "";
        }

        public static bool IsDefaultValue(PropertyInfo property, object @object)
        {
            foreach (var attrib in (property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[])
            {
                if ((attrib.Type == null) || (@object == null) || attrib.Type.Equals(@object.GetType()))
                {
                    if (@object == attrib.DefaultValue)
                    {
                        return true;
                    }

                    if (@object == null)
                    {
                        return false;
                    }

                    if (attrib.DefaultValue == null)
                    {
                        return false;
                    }

                    return @object.Equals(attrib.DefaultValue);
                }
            }

            return false;
        }
    }
}
