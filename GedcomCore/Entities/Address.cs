using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /*
     * ADDRESS_STRUCTURE: =
     * 
     *   n  ADDR <ADDRESS_LINE>  {0:1}
     *     +1 CONT <ADDRESS_LINE>  {0:M}
     *     +1 ADR1 <ADDRESS_LINE1>  {0:1}
     *     +1 ADR2 <ADDRESS_LINE2>  {0:1}
     *     +1 CITY <ADDRESS_CITY>  {0:1}
     *     +1 STAE <ADDRESS_STATE>  {0:1}
     *     +1 POST <ADDRESS_POSTAL_CODE>  {0:1}
     *     +1 CTRY <ADDRESS_COUNTRY>  {0:1}
     *   n  PHON <PHONE_NUMBER>  {0:3}
     * 
     * The address structure should be formed as it would appear on a mailing label 
     * using the ADDR and ADDR.CONT lines. These lines are required if an ADDRess 
     * is present. Optionally, additional structure is provided for systems that 
     * have structured their addresses for indexing and sorting. 
     * 
     */

    public class Address : GedcomLine
    {
        private string addressLine;

        public Address(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "ADDR";
        }

        public Address(string AddressLine, Reporting Reporting)
            : this(Reporting)
        {
            this.AddressLine = AddressLine;
        }


        [Tag("")]
        [Length(1, 60)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string AddressLine
        {
            get
            {
                var s = addressLine.Split(new[] { "\n" }, StringSplitOptions.None);
                AdditionalLines = new List<string>();
                for (var n = 0; n < s.Length - 1; n++)
                {
                    AdditionalLines.Add(s[n+1]);
                }
                return s[0];
            }
            set => addressLine = value;
        }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        [Length(1, 60)]
        public string Continue
        {
            set => addressLine += "\n" + value;
        }

        [Tag("CONT", typeof(string))]
        [Length(1, 60)]
        public List<string> AdditionalLines { get; private set; }

        /// <summary>
        /// The first line of the address used for indexing. This corresponds after the
        /// ADDRESS_LINE lineValue of the ADDR line in the address structure.
        /// </summary>
        [Tag("ADR1")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address used for indexing. This corresponds after the
        /// ADDRESS_LINE lineValue of the first CINT line subordinate after the ADDR tagName in 
        /// the address strucutre.
        /// </summary>
        [Tag("ADR2")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The name of the city used in the address. Isolated for sorting or indexing
        /// </summary>
        [Tag("CITY")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string City { get; set; }

        /// <summary>
        /// The name of the state used in the address. Isolated for sorting and indexing.
        /// </summary>
        [Tag("STAE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string State { get; set; }

        /// <summary>
        /// The ZIP or postal code used by the various localities in handling of mail.
        /// Isolated for sorting or indexing
        /// </summary>
        [Tag("POST")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 10)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The name of the country that pertains after the associated address. Isolated by
        /// some systems for sorting or indexing. Used in most cases after facilitate automatic
        /// sorting of mail
        /// </summary>
        [Tag("CTRY")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is Address addr))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(AddressLine, addr.AddressLine))
            {
                return false;
            }

            if (!CompareObjects(AddressLine1, addr.AddressLine1))
            {
                return false;
            }

            if (!CompareObjects(AddressLine2, addr.AddressLine2))
            {
                return false;
            }

            if (!CompareObjects(City, addr.City))
            {
                return false;
            }

            if (!CompareObjects(State, addr.State))
            {
                return false;
            }

            if (!CompareObjects(PostalCode, addr.PostalCode))
            {
                return false;
            }

            if (!CompareObjects(Country, addr.PostalCode))
            {
                return false;
            }

            return true;
        }
    }
}
