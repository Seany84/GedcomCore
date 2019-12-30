using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     *   n  [ BAPL | CONL ]  {1:1}
     *     +1 STAT <LDS_BAPTISM_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */

    class LdsIndividualOrdianceBaptism : LdsIndividualOrdiance
    {
        private LdsBaptismDateStatus baptismDateStatus;

        public LdsIndividualOrdianceBaptism(Reporting Reporting)
            : base(Reporting)
        {
            baptismDateStatus = LdsBaptismDateStatus.Unknown;
        }

        /* in TGC551, line 785
         * 1 CONL Y
         * TODO: what does that mean?
         * */
        public LdsIndividualOrdianceBaptism(string Str, Reporting Reporting) 
            : base(Reporting)
        {

        }

        [Tag("STAT", LdsBaptismDateStatus.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public LdsBaptismDateStatus LdsBaptismDateStatus
        {
            get
            {
                return baptismDateStatus;
            }
            set
            {
                baptismDateStatus = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            LdsIndividualOrdianceBaptism baptism = obj as LdsIndividualOrdianceBaptism;
            if (baptism == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(LdsBaptismDateStatus, baptism.LdsBaptismDateStatus))
            {
                return false;
            }

            return true;
        }
    }
}
