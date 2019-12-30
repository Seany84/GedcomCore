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
        private string type;
        private DateValue date;
        private Place place;
        private Address address;
        private List<string> phoneNumbers;
        private AgeAtEvent ageAtEvent;
        private string responsibleAgency;
        private string cause;
        private List<SourceCitation> sourceCitations;
        private List<MultimediaLink> multimedia;
        private List<NoteStructure> notes;

        protected EventDetail(Reporting Reporting)
            : base(Reporting)
        {
            phoneNumbers = new List<string>();
            sourceCitations = new List<SourceCitation>();
            multimedia = new List<MultimediaLink>();
            notes = new List<NoteStructure>();
        }

        [Tag("TYPE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string DateString
        {
            get
            {
                if (date == null)
                {
                    return null;
                }

                return date.DateString;
            }
            set
            {
                date = DateValue.CreateDateValue(value, Reporting);
            }
        }

        public DateValue Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Place Place
        {
            get
            {
                return place;
            }
            set
            {
                place = value;
            }
        }

        [Tag("ADDR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        [Tag("PHON", typeof(string))]
        [Quantity(0, 3)]
        [Length(1, 25)]
        public List<string> PhoneNumbers
        {
            get
            {
                return phoneNumbers;
            }
            set
            {
                phoneNumbers = value;
            }
        }
        
        [Tag("AGE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public AgeAtEvent AgeAtEvent
        {
            get
            {
                return ageAtEvent;
            }
            set
            {
                ageAtEvent = value;
            }
        }

        [Tag("AGNC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string ResponsibleAgency
        {
            get
            {
                return responsibleAgency;
            }
            set
            {
                responsibleAgency = value;
            }
        }

        [Tag("CAUS")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Cause
        {
            get
            {
                return cause;
            }
            set
            {
                cause = value;
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

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia
        {
            get
            {
                return multimedia;
            }
            set
            {
                multimedia = value;
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

            EventDetail d = obj as EventDetail;
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
