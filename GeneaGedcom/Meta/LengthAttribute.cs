using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom.Meta
{
    /// <summary>
    /// specifies minimum and maximum length of the gedcom data
    /// </summary>
    class LengthAttribute : Attribute
    {
        /// <summary>
        /// the minimum length of the gedcom data
        /// </summary>
        private readonly int minimum;

        /// <summary>
        /// the maximum length of the gedcom data
        /// </summary>
        private readonly int maximum;

        /// <summary>
        /// creates a new LengthAttribute
        /// </summary>
        /// <param name="Minimum">the minimum length of the gedcom data</param>
        /// <param name="Maximum">the maximum length of the gedcom data</param>
        public LengthAttribute(int Minimum, int Maximum)
        {
            minimum = Minimum;
            maximum = Maximum;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId
        {
            get
            {
                return "Length";
            }
        }

        /// <summary>
        /// returns the maximum length of the gedcom data
        /// </summary>
        public int Maximum
        {
            get
            {
                return maximum;
            }
        }

        /// <summary>
        /// return the minimum length of the gedcom data
        /// </summary>
        public int Minimum
        {
            get
            {
                return minimum;
            }
        }
    }
}