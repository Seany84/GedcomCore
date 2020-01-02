using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// specifies minimum and maximum length of the gedcom data
    /// </summary>
    internal class LengthAttribute : Attribute
    {
        /// <summary>
        /// creates a new LengthAttribute
        /// </summary>
        /// <param name="minimum">the minimum length of the gedcom data</param>
        /// <param name="maximum">the maximum length of the gedcom data</param>
        public LengthAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId => "Length";

        /// <summary>
        /// returns the maximum length of the gedcom data
        /// </summary>
        public int Maximum { get; }

        /// <summary>
        /// return the minimum length of the gedcom data
        /// </summary>
        public int Minimum { get; }
    }
}
