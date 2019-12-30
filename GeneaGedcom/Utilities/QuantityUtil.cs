using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using GeneaGedcom.Meta;

namespace GeneaGedcom.Utilities
{
    /// <summary>
    /// contains several useful functions for dealing with quantity restrictions
    /// </summary>
    public class QuantityUtil
    {
        /// <summary>
        /// the default maximum number of occurances
        /// </summary>
        private static readonly int defaultMaximum = 1;

        /// <summary>
        /// the default minimum number of occurances
        /// </summary>
        private static readonly int defaultMinimum = 1;

        /// <summary>
        /// returns the maximum number of occurances of the given property
        /// </summary>
        /// <param name="Property">a property</param>
        /// <returns>the maximum number of occurances of the given property</returns>
        public static int GetMaximum(PropertyInfo Property)
        {
            var attributes = Property.GetCustomAttributes(typeof(QuantityAttribute), true) as QuantityAttribute[];
            if (attributes.Length == 0)
            {
                return defaultMaximum;
            }
            if (attributes.Length > 1)
            {
                throw new InternalException("quantity attribute applied more than once.");
            }
            else
            {
                return attributes[0].Maximum;
            }
        }

        /// <summary>
        /// returns the minimum number of occurances of the given property
        /// </summary>
        /// <param name="Property">a property</param>
        /// <returns>the minimum number of occurances of the given property</returns>
        public static int GetMinimum(PropertyInfo Property)
        {
            var attributes = Property.GetCustomAttributes(typeof(QuantityAttribute), true) as QuantityAttribute[];
            if (attributes.Length == 0)
            {
                return defaultMinimum;
            }
            if (attributes.Length > 1)
            {
                throw new InternalException("quantity attribute applied more than once.");
            }
            else
            {
                return attributes[0].Minimum;
            }
        }
    }
}
