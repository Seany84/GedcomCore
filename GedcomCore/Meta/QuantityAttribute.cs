using System;

namespace GedcomCore.Framework.Meta
{
    /// <summary>
    /// annotates how often a gedcom tag may or must occur
    /// </summary>
    internal class QuantityAttribute : Attribute
    {
        /// <summary>
        /// creates a new QuantityAttribute
        /// </summary>
        /// <param name="minimum">the minimal number of occurrences</param>
        /// <param name="maximum">the maximal number of occurrences</param>
        public QuantityAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
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
                    Minimum = 0;
                    Maximum = 1;
                    break;

                case PredefinedQuantities.OneRequired:
                    Minimum = 1;
                    Maximum = 1;
                    break;

                case PredefinedQuantities.OneUnbounded:
                    Minimum = 1;
                    Maximum = int.MaxValue;
                    break;

                case PredefinedQuantities.Unbounded:
                    Minimum = 0;
                    Maximum = int.MaxValue;
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
        public int Minimum { get; }

        /// <summary>
        /// returns the maximal number of occurrences
        /// </summary>
        public int Maximum { get; }

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
