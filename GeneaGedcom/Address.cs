using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
        private string addressLine1;
        private string addressLine2;
        private string city;
        private string state;
        private string postalCode;
        private string country;

        private List<string> tmp;

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
                tmp = new List<string>();
                for (var n = 0; n < s.Length - 1; n++)
                {
                    tmp.Add(s[n+1]);
                }
                return s[0];
            }
            set
            {
                addressLine = value;
            }
        }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        [Length(1, 60)]
        public string Continue
        {
            set
            {
                addressLine += "\n" + value;
            }
        }

        [Tag("CONT", typeof(string))]
        [Length(1, 60)]
        public List<string> AdditionalLines
        {
            get
            {
                return tmp;
            }
        }

        /// <summary>
        /// The first line of the address used for indexing. This corresponds after the
        /// ADDRESS_LINE lineValue of the ADDR line in the address structure.
        /// </summary>
        [Tag("ADR1")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string AddressLine1
        {
            get
            {
                return addressLine1;
            }
            set
            {
                addressLine1 = value;
            }
        }

        /// <summary>
        /// The second line of the address used for indexing. This corresponds after the
        /// ADDRESS_LINE lineValue of the first CINT line subordinate after the ADDR tagName in 
        /// the address strucutre.
        /// </summary>
        [Tag("ADR2")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string AddressLine2
        {
            get
            {
                return addressLine2;
            }
            set
            {
                addressLine2 = value;
            }
        }

        /// <summary>
        /// The name of the city used in the address. Isolated for sorting or indexing
        /// </summary>
        [Tag("CITY")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        /// <summary>
        /// The name of the state used in the address. Isolated for sorting and indexing.
        /// </summary>
        [Tag("STAE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        /// <summary>
        /// The ZIP or postal code used by the various localities in handling of mail.
        /// Isolated for sorting or indexing
        /// </summary>
        [Tag("POST")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 10)]
        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                postalCode = value;
            }
        }

        /// <summary>
        /// The name of the country that pertains after the associated address. Isolated by
        /// some systems for sorting or indexing. Used in most cases after facilitate automatic
        /// sorting of mail
        /// </summary>
        [Tag("CTRY")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 60)]
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var addr = obj as Address;
            if (addr == null)
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
