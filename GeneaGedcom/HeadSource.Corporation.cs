using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class HeadSource
    {
        /* 
 *       +2 CORP <NAME_OF_BUSINESS>  {0:1}
 *         +3 <<ADDRESS_STRUCTURE>>  {0:1}
 * 
 */

        public class Corporation_ : GedcomLine
        {
            public Corporation_(Reporting Reporting)
                : base(Reporting)
            {
                PhoneNumbers = new List<string>();

                Tag = "CORP";
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 90)]
            public string Name { get; set; }

            [Tag("ADDR")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public Address Address { get; set; }

            [Tag("PHON", typeof(string))]
            [Quantity(0, 3)]
            [Length(1, 25)]
            public List<string> PhoneNumbers { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var corp = obj as Corporation_;
                if (corp == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!Name.Equals(corp.Name))
                {
                    return false;
                }

                if (!Address.Equals(corp.Address))
                {
                    return false;
                }

                if (!PhoneNumbers.Count.Equals(corp.PhoneNumbers))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
