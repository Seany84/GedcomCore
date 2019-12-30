using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * LDS_INDIVIDUAL_ORDINANCE: =
     * 
     *   [
     *   n  [ BAPL | CONL ]  {1:1}
     *     +1 STAT <LDS_BAPTISM_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   |
     *   n  ENDL          {1:1}
     *     +1 STAT <LDS_ENDOWMENT_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   |
     *   n  SLGC          {1:1}
     *     +1 STAT <LDS_CHILD_SEALING_DATE_STATUS>  {0:1}
     *     +1 DATE <DATE_LDS_ORD>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 PLAC <PLACE_LIVING_ORDINANCE>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {1:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   ]
     * 
     */

    public class LdsIndividualOrdiance : GedcomLine
    {
        private DateValue dateLdsOrdiance;
        private string templeCode;
        private string placeLivingOrdiance;
        private List<SourceCitation> sourceCitations;
        private List<NoteStructure> notes;

        public LdsIndividualOrdiance(Reporting Reporting)
            : base(Reporting)
        {
            sourceCitations = new List<SourceCitation>();
            notes = new List<NoteStructure>();
        }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string DateLdsOrdianceString
        {
            get
            {
                return dateLdsOrdiance == null ? "" : dateLdsOrdiance.DateString;
            }
            set
            {
                dateLdsOrdiance = DateValue.CreateDateValue(value, Reporting);
            }
        }

        public DateValue DateLdsOrdiance
        {
            get
            {
                return dateLdsOrdiance;
            }
            set
            {
                dateLdsOrdiance = value;
            }
        }

        [Tag("TEMP")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(4,5)]
        public string TempleCode
        {
            get
            {
                return templeCode;
            }
            set
            {
                templeCode = value;
            }
        }

        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string PlaceLivingOrdiance
        {
            get
            {
                return placeLivingOrdiance;
            }
            set
            {
                placeLivingOrdiance = value;
            }
        }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations
        {
            get
            {
                return sourceCitations;
            }
            set
            {
                sourceCitations = value;
            }
        }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            LdsIndividualOrdiance lds = obj as LdsIndividualOrdiance;
            if (lds == null)
            {
                return false;
            }

            if (!base.Equals(obj))
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
