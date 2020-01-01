using System.Reflection;
using GedcomCore.Framework.Entities;
using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Utilities
{
    /// <summary>
    /// contains several useful functions for dealing with length restrictions
    /// </summary>
    class LengthUtil
    {
        /// <summary>
        /// returns the default maximum length
        /// </summary>
        private static readonly int defaultMaximumLength = int.MaxValue;

        /// <summary>
        /// returns the default minimum length
        /// </summary>
        private static readonly int defaultMinimumLength = 0;

        /// <summary>
        /// returns the maximum length of the given property
        /// </summary>
        /// <param name="Property">a propery</param>
        /// <returns>the maximum length of the gedcom data</returns>
        public static int GetMaximumLength(PropertyInfo Property)
        {
            var attributes = Property.GetCustomAttributes(typeof(LengthAttribute), true) as LengthAttribute[];
            if (attributes.Length == 0)
            {
                return defaultMaximumLength;
            }
            if (attributes.Length > 1)
            {
                throw new InternalException("length attribute applied more than once.");
            }
            else
            {
                return attributes[0].Maximum;
            }
        }

        /// <summary>
        /// returns the mimimum length of the given property
        /// </summary>
        /// <param name="Property">a propery</param>
        /// <returns>the mimimum length of the gedcom data</returns>
        public static int GetMinimumLength(PropertyInfo Property)
        {
            var attributes = Property.GetCustomAttributes(typeof(LengthAttribute), true) as LengthAttribute[];
            if (attributes.Length == 0)
            {
                return defaultMinimumLength;
            }
            if (attributes.Length > 1)
            {
                throw new InternalException("length attribute applied more than once.");
            }
            else
            {
                return attributes[0].Minimum;
            }
        }
    }
}
