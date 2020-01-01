using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Lds
{
    /* 
     *   n  ENDL          {1:1}
     *     +1 STAT <LDS_ENDOWMENT_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */

    class LdsIndividualOrdianceEndowment : LdsIndividualOrdiance
    {
        public LdsIndividualOrdianceEndowment(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "ENDL";

            LdsEndowmentDateStatus = LdsEndowmentDateStatus.Unknown;
        }

        [Tag("STAT", LdsEndowmentDateStatus.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public LdsEndowmentDateStatus LdsEndowmentDateStatus { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var lds = obj as LdsIndividualOrdianceEndowment;
            if (lds == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(LdsEndowmentDateStatus, lds.LdsEndowmentDateStatus))
            {
                return false;
            }

            return true;
        }
    }
}
