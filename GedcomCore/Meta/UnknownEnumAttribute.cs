using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// attribute that indicates, that an enum-value is the default value of the enumeration
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class UnknownEnumAttribute : Attribute
    {
        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "UnknownEnum";
    }
}
