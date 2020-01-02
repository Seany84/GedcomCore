using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// assigns tags to properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    internal class TagAttribute : Attribute
    {
        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="tagName">the tag name</param>
        public TagAttribute(string tagName)
        {
            TagName = tagName;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="tagName">the tag name</param>
        /// <param name="defaultValue">the default value for the field. refer to the <see cref="DefaultValue"/>DefaultValue property for details</param>
        public TagAttribute(string tagName, object defaultValue)
            : this(tagName)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="tagName">the tag name</param>
        /// <param name="type">the type of the gedcom data.
        /// 
        /// this can differ from the properties type when
        /// * the property is a container containing objects of the giventype
        /// * a property has multiple tags, each referencing to another type which is derived from the property's type
        /// </param>
        public TagAttribute(string tagName, Type type) 
            : this(tagName)
        {
            Type = type;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="tagName">the tag name</param>
        /// <param name="type">the type of the gedcom data</param>
        /// <param name="defaultValue">the default value for the field. refer to the <see cref="DefaultValue"/>DefaultValue property for details</param>
        public TagAttribute(string tagName, Type type, object defaultValue)
            : this(tagName, type)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "Tag";

        /// <summary>
        /// return the tag name
        /// </summary>
        public string TagName { get; }

        /// <summary>
        /// the type of the gedcom data.
        /// 
        /// this can differ from the properties type when
        /// * the property is a container containing objects of the giventype
        /// * a property has multiple tags, each referencing to another type which is derived from the property's type
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// indicates the default value, that should 
        /// * not be written by the GedcomWriter
        /// * not be interpreted by client code, as is indicates, the the parser has no read a value for the field
        /// 
        /// This is especially important for ValueType fields, as they can not have a null value
        /// </summary>
        public object DefaultValue { get; }
    }
}
