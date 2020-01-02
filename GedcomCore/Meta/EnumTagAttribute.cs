using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// assigns tags to enum fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class EnumTagAttribute : Attribute
    {
        /// <summary>
        /// creates a new EnumTagAttribute
        /// </summary>
        /// <param name="tagName">the tag name</param>
        public EnumTagAttribute(string tagName)
        {
            TagName = tagName;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "EnumTag";

        /// <summary>
        /// returns the tag name
        /// </summary>
        public string TagName { get; }
    }
}
