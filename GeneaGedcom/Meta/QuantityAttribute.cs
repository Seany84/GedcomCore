using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// annotates how often a gedcom tag may or must occur
    /// </summary>
    class QuantityAttribute : Attribute
    {
        /// <summary>
        /// the minimal number of occurrences
        /// </summary>
        private readonly int minimum;

        /// <summary>
        /// the maximal number of occurrences
        /// </summary>
        private readonly int maximum;

        /// <summary>
        /// creates a new QuantityAttribute
        /// </summary>
        /// <param name="Minimum">the minimal number of occurrences</param>
        /// <param name="Maximum">the maximal number of occurrences</param>
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
        public override object TypeId => "Quantity";

        /// <summary>
        /// returns the minimal number of occurrences
        /// </summary>
        public int Minimum => minimum;

        /// <summary>
        /// returns the maximal number of occurrences
        /// </summary>
        public int Maximum => maximum;

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
