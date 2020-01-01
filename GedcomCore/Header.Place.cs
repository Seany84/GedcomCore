using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    public partial class Header
    {
        public class Place_ : GedcomLine
        {
            public Place_(Reporting Reporting)
                : base(Reporting)
            {
            }

            [Tag("FORM")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 120)]
            public string PlaceHierarchy { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var place = obj as Place_;
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
