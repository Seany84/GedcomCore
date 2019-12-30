using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class IndividualEventAdoption
    {
        public class AdoptionFamily_ : GedcomLine
        {
            private string familyXRef;
            private AdoptedByWhichParent adoptedByWhichParent;

            public AdoptionFamily_(Reporting Reporting)
                : base(Reporting)
            {
                adoptedByWhichParent = AdoptedByWhichParent.Unknown;
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string FamilyXRef
            {
                get
                {
                    return familyXRef;
                }
                set
                {
                    familyXRef = value;
                }
            }

            [Tag("ADOP", AdoptedByWhichParent.Unknown)]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public AdoptedByWhichParent AdoptedByWhichParent
            {
                get
                {
                    return adoptedByWhichParent;
                }
                set
                {
                    adoptedByWhichParent = value;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                AdoptionFamily_ fam = obj as AdoptionFamily_;
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
