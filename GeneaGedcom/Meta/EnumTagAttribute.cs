using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// assigns tags to enum fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumTagAttribute : Attribute
    {
        /// <summary>
        /// the tag name
        /// </summary>
        private readonly string tagName;

        /// <summary>
        /// creates a new EnumTagAttribute
        /// </summary>
        /// <param name="TagName">the tag name</param>
        public EnumTagAttribute(string TagName)
        {
            tagName = TagName;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "EnumTag";

        /// <summary>
        /// returns the tag name
        /// </summary>
        public string TagName => tagName;
    }
}
