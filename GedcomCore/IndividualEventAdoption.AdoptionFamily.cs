using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    public partial class IndividualEventAdoption
    {
        public class AdoptionFamily_ : GedcomLine
        {
            public AdoptionFamily_(Reporting Reporting)
                : base(Reporting)
            {
                AdoptedByWhichParent = AdoptedByWhichParent.Unknown;
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string FamilyXRef { get; set; }

            [Tag("ADOP", AdoptedByWhichParent.Unknown)]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public AdoptedByWhichParent AdoptedByWhichParent { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var fam = obj as AdoptionFamily_;
                if (fam == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!CompareObjects(FamilyXRef, fam.FamilyXRef))
                {
                    return false;
                }

                if (!CompareObjects(AdoptedByWhichParent, fam.AdoptedByWhichParent))
                {
                    return false;
                }

                return true;
            }
        }
    }
}