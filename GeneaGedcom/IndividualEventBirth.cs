using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     *   n[ BIRT | CHR ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {0:1}
     * 
     */

    public class IndividualEventBirth : IndividualEvent
    {
        private string familyXRef;

        public IndividualEventBirth(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string FamilyXref
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            IndividualEventBirth birth = obj as IndividualEventBirth;
            if (birth == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(FamilyXref, birth.FamilyXref))
            {
                return false;
            }

            return true;
        }
    }
}
