using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom.Meta
{
    /// <summary>
    /// annotates how often a gedcom tag may or must occur
    /// </summary>
    class QuantityAttribute : Attribute
    {
        /// <summary>
        /// the minimal number of occurances
        /// </summary>
        private readonly int minimum;

        /// <summary>
        /// the maximal number of occurances
        /// </summary>
        private readonly int maximum;

        /// <summary>
        /// creates a new QuantityAttribute
        /// </summary>
        /// <param name="Minimum">the minimal number of occurances</param>
        /// <param name="Maximum">the maximal number of occurances</param>
        public QuantityAttribute(int Minimum, int Maximum)
        {
            minimum = Minimum;
            maximum = Maximum;
        }

        /// <summary>
        /// creates a new QuantitiyAttribute by using predefined quantities
        /// </summary>
        /// <param name="Predefined">a predefined quantity</param>
        public QuantityAttribute(QuantityAttribute.PredefinedQuantities Predefined)
        {
            switch (Predefined)
            {
                case PredefinedQuantities.OneOptional:
                    minimum = 0;
                    maximum = 1;
                    break;

                case PredefinedQuantities.OneRequired:
                    minimum = 1;
                    maximum = 1;
                    break;

                case PredefinedQuantities.OneUnbounded:
                    minimum = 1;
                    maximum = int.MaxValue;
                    break;

                case PredefinedQuantities.Unbounded:
                    minimum = 0;
                    maximum = int.MaxValue;
                    break;
            }
        }

        /// <summary>
        /// TypeId
        /// </summary>
        public override object TypeId
        {
            get
            {
                return "Quantity";
            }
        }

        /// <summary>
        /// returns the minimal number of occurances
        /// </summary>
        public int Minimum
        {
            get
            {
                return minimum;
            }
        }

        /// <summary>
        /// returns the maximal number of occurances
        /// </summary>
        public int Maximum
        {
            get
            {
                return maximum;
            }
        }

        /// <summary>
        /// predefined quantities
        /// </summary>
        public enum PredefinedQuantities
        {
            /// <summary>
            /// exactly one
            /// </summary>
            OneRequired,

            /// <summary>
            /// zero or one
            /// </summary>
            OneOptional,

            /// <summary>
            /// zero or more
            /// </summary>
            Unbounded,

            /// <summary>
            /// one or more
            /// </summary>
            OneUnbounded
        }
    }
}
