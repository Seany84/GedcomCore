using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class Header
    {
        public class Place_ : GedcomLine
        {
            private string placeHierarchy;

            public Place_(Reporting Reporting)
                : base(Reporting)
            {
            }

            [Tag("FORM")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 120)]
            public string PlaceHierarchy
            {
                get
                {
                    return placeHierarchy;
                }
                set
                {
                    placeHierarchy = value;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                Place_ place = obj as Place_;
                if (place == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!PlaceHierarchy.Equals(place.PlaceHierarchy))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
