using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    /* 
     * LDS_SPOUSE_SEALING: =
     * 
     *   n  SLGS          {1:1}
     *     +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */

    public class LdsSpouseSealing : GedcomLine
    {
        public LdsSpouseSealing(Reporting Reporting)
            : base(Reporting)
        {
            SourceCitations = new List<SourceCitation>();
            Notes = new List<NoteStructure>();

            Tag = "SLGS";

            LdsSpouseSealingDateStatus = LdsSpouseSealingDateStatus.Uncleared;
        }

        [Tag("STAT", LdsSpouseSealingDateStatus.Uncleared)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public LdsSpouseSealingDateStatus LdsSpouseSealingDateStatus { get; set; }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string DateLdsOrdianceString
        {
            get => DateLdsOrdiance.DateString;
            set => DateLdsOrdiance = DateValue.CreateDateValue(value, Reporting);
        }

        public DateValue DateLdsOrdiance { get; set; }

        [Tag("TEMP")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(4,5)]
        public string TempleCode { get; set; }

        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string PlaceLivingOrdiance { get; set; }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var lds = obj as LdsSpouseSealing;
            if (lds == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(LdsSpouseSealingDateStatus, lds.LdsSpouseSealingDateStatus))
            {
                return false;
            }

            if (!CompareObjects(DateLdsOrdiance, lds.DateLdsOrdiance))
            {
                return false;
            }

            if (!CompareObjects(TempleCode, lds.TempleCode))
            {
                return false;
            }

            if (!CompareObjects(PlaceLivingOrdiance, lds.PlaceLivingOrdiance))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(lds.SourceCitations.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(lds.Notes.Count))
            {
                return false;
            }

            return true;
        }
    }
}
