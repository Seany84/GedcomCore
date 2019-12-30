using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom.Utilities
{
    /// <summary>
    /// contains several useful functions for dealing with tags
    /// </summary>
    public class TagUtil
    {
        /// <summary>
        /// returns the property of the given object, that holds objects for the given tag name
        /// 
        /// throws
        /// - InternalException if more than one suitable properties are found
        /// - MemberNotFoundException if no suitable property is found
        /// </summary>
        /// <param name="Object">object that is considered</param>
        /// <param name="TagName">tag name of the searched property</param>
        /// <param name="MustBeReadable">indicates if the found property must be readable in order to be considered</param>
        /// <param name="MustBeWriteable">indicates if the found property must be writeable in order to be considered</param>
        /// <returns>one of Object's proerties that matches the given criteria</returns>
        public static PropertyInfo GetMember(object Object, string TagName, bool MustBeReadable, bool MustBeWriteable)
        {
            var members = new List<PropertyInfo>();

            foreach (var member in Object.GetType().GetProperties())
            {
                foreach (var attrib in ((member.GetCustomAttributes(typeof(TagAttribute), false)) as TagAttribute[]))
                {
                    if (attrib.TagName == TagName)
                    {
                        if (MustBeReadable && !member.CanRead)
                        {
                            continue;
                        }

                        if (MustBeWriteable && !member.CanWrite)
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
                throw new InternalException("more than one properties are tagged with " + TagName);
            }
            if (members.Count == 0)
            {
                throw new MemberNotFoundException("no property is tagged with " + TagName, TagName);
            }
            return members[0];
        }

        /// <summary>
        /// returns the type for the given property and tag name
        /// </summary>
        /// <param name="Property">property for which the type shall be determined</param>
        /// <param name="TagName">one of the tag names for the given properties</param>
        /// <returns>the type for the given property and tag name</returns>
        public static Type GetLineType(PropertyInfo Property, string TagName)
        {
            foreach(var attrib in ((Property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[]))
            {
                if (string.Compare(attrib.TagName, TagName, true) == 0)
                {
                    if (attrib.Type != null)
                    {
                        return attrib.Type;
                    }
                }
            }

            return Property.PropertyType;
        }

        /// <summary>
        /// returns an enumeration with all tagged properties of the given type with their type-tag combinations
        /// </summary>
        /// <param name="Type">a type</param>
        /// <returns>all tagged properties of the given type with their type-tag combinations</returns>
        public static IEnumerable<KeyValuePair<PropertyInfo, IDictionary<Type, string>>> GetTags(Type Type)
        {
            foreach(var prop in Type.GetProperties())
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

        public static string GetTagName(PropertyInfo Property)
        {
            foreach (var attrib in (Property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[])
            {
                return attrib.TagName;
            }

            return "";
        }

        public static bool IsDefaultValue(PropertyInfo Property, object Object)
        {
            foreach (var attrib in (Property.GetCustomAttributes(typeof(TagAttribute), true)) as TagAttribute[])
            {
                if ((attrib.Type == null) || (Object == null) || attrib.Type.Equals(Object.GetType()))
                {
                    if (Object == attrib.DefaultValue)
                    {
                        return true;
                    }

                    if (Object == null)
                    {
                        return false;
                    }

                    if (attrib.DefaultValue == null)
                    {
                        return false;
                    }

                    return Object.Equals(attrib.DefaultValue);
                }
            }

            return false;
        }
    }
}
