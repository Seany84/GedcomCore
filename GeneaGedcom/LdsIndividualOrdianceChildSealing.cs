using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     *   n  SLGC          {1:1}
     *     +1 STAT <LDS_CHILD_SEALING_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {1:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */

    class LdsIndividualOrdianceChildSealing : LdsIndividualOrdiance
    {
        private LdsChildSealingDateStatus ldsChildSealingDateStatus;
        private string familyXRef;

        public LdsIndividualOrdianceChildSealing(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "SLGC";

            ldsChildSealingDateStatus = LdsChildSealingDateStatus.Unknown;
        }

        [Tag("STAT", LdsChildSealingDateStatus.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public LdsChildSealingDateStatus LdsChildSealingDateStatus
        {
            get => ldsChildSealingDateStatus;
            set => ldsChildSealingDateStatus = value;
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string FamilyXRef
        {
            get => familyXRef;
            set => familyXRef = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var lds = obj as LdsIndividualOrdianceChildSealing;
            if (lds == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(LdsChildSealingDateStatus, lds.LdsChildSealingDateStatus))
            {
                return false;
            }

            if (!CompareObjects(FamilyXRef, lds.FamilyXRef))
            {
                return false;
            }

            return true;
        }
    }
}
