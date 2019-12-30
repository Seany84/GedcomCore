using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom.Meta
{
    /// <summary>
    /// assigns tags to properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    class TagAttribute : Attribute
    {
        /// <summary>
        /// the tagname
        /// </summary>
        private readonly string tagName;

        /// <summary>
        /// the type of the gedcom data.
        /// 
        /// this can differ from the properties type when
        /// * the property is a container containing objects of the giventype
        /// * a property has multiple tags, each referencing to another type which is derived from the property's type
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// indicates the default value, that should 
        /// * not be written by the GedcomWriter
        /// * not be interpreted by client code, as is indicates, the the parser has no read a value for the field
        /// 
        /// This is especially important for ValueType fields, as they can not have a null value
        /// </summary>
        private readonly object defaultValue = null;

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="TagName">the tag name</param>
        public TagAttribute(string TagName)
        {
            tagName = TagName;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="TagName">the tag name</param>
        /// <param name="DefaultValue">the default value for the field. refer to the <see cref="DefaultValue"/>DefaultValue property for details</param>
        public TagAttribute(string TagName, object DefaultValue)
            : this(TagName)
        {
            defaultValue = DefaultValue;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="TagName">the tag name</param>
        /// <param name="Type">the type of the gedcom data.
        /// 
        /// this can differ from the properties type when
        /// * the property is a container containing objects of the giventype
        /// * a property has multiple tags, each referencing to another type which is derived from the property's type
        /// </param>
        public TagAttribute(string TagName, Type Type) 
            : this(TagName)
        {
            type = Type;
        }

        /// <summary>
        /// creates a new TagAttribute
        /// </summary>
        /// <param name="TagName">the tag name</param>
        /// <param name="Type">the type of the gedcom data</param>
        /// <param name="DefaultValue">the default value for the field. refer to the <see cref="DefaultValue"/>DefaultValue property for details</param>
        public TagAttribute(string TagName, Type Type, object DefaultValue)
            : this(TagName, Type)
        {
            defaultValue = DefaultValue;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "Tag";

        /// <summary>
        /// return the tag name
        /// </summary>
        public string TagName => tagName;

        /// <summary>
        /// the type of the gedcom data.
        /// 
        /// this can differ from the properties type when
        /// * the property is a container containing objects of the giventype
        /// * a property has multiple tags, each referencing to another type which is derived from the property's type
        /// </summary>
        public Type Type => type;

        /// <summary>
        /// indicates the default value, that should 
        /// * not be written by the GedcomWriter
        /// * not be interpreted by client code, as is indicates, the the parser has no read a value for the field
        /// 
        /// This is especially important for ValueType fields, as they can not have a null value
        /// </summary>
        public object DefaultValue => defaultValue;
    }
}
