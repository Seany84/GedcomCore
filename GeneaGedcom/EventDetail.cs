using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * EVENT_DETAIL: =
     * 
     *   n  TYPE <EVENT_DESCRIPTOR>  {0:1}
     *   n  DATE <DATE_VALUE>  {0:1}
     *   n  <<PLACE_STRUCTURE>>  {0:1}
     *   n  <<ADDRESS_STRUCTURE>>  {0:1}
     *   n  AGE <AGE_AT_EVENT>  {0:1}
     *   n  AGNC <RESPONSIBLE_AGENCY>  {0:1}
     *   n  CAUS <CAUSE_OF_EVENT>  {0:1}
     *   n  <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *   n  <<MULTIMEDIA_LINK>>  {0:M}
     *   n  <<NOTE_STRUCTURE>>  {0:M}
     * 
     */
 
    public class EventDetail : GedcomLine
    {
        protected EventDetail(Reporting Reporting)
            : base(Reporting)
        {
            PhoneNumbers = new List<string>();
            SourceCitations = new List<SourceCitation>();
            Multimedia = new List<MultimediaLink>();
            Notes = new List<NoteStructure>();
        }

        [Tag("TYPE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Type { get; set; }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string DateString
        {
            get
            {
                if (Date == null)
                {
                    return null;
                }

                return Date.DateString;
            }
            set => Date = DateValue.CreateDateValue(value, Reporting);
        }

        public DateValue Date { get; set; }

        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Place Place { get; set; }

        [Tag("ADDR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Address Address { get; set; }

        [Tag("PHON", typeof(string))]
        [Quantity(0, 3)]
        [Length(1, 25)]
        public List<string> PhoneNumbers { get; set; }

        [Tag("AGE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public AgeAtEvent AgeAtEvent { get; set; }

        [Tag("AGNC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string ResponsibleAgency { get; set; }

        [Tag("CAUS")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Cause { get; set; }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var d = obj as EventDetail;
            if (d == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Type, d.Type))
            {
                return false;
            }

            if (!CompareObjects(Date, d.Date))
            {
                return false;
            }

            if (!CompareObjects(Place, d.Place))
            {
                return false;
            }

            if (!CompareObjects(Address, d.Address))
            {
                return false;
            }

            if (!PhoneNumbers.Count.Equals(d.PhoneNumbers.Count))
            {
                return false;
            }

            if (!CompareObjects(AgeAtEvent, d.AgeAtEvent))
            {
                return false;
            }

            if (!CompareObjects(ResponsibleAgency, d.ResponsibleAgency))
            {
                return false;
            }

            if (!CompareObjects(Cause, d.Cause))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(d.SourceCitations.Count))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(d.Multimedia.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(d.Notes.Count))
            {
                return false;
            }

            return true;
        }
    }
}
