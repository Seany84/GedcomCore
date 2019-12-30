using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom.Meta
{
    /// <summary>
    /// attribute that indicates, that an enum-value is the default value of the enumeration
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    class UnknownEnumAttribute : Attribute
    {
        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "UnknownEnum";
    }
}
