using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * MULTIMEDIA_RECORD: =
     * 
     *   n @<XREF:OBJE>@ OBJE  {1:1}
     *     +1 FORM <MULTIMEDIA_FORMAT>  {1:1}
     *     +1 TITL <DESCRIPTIVE_TITLE>  {0:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 BLOB        {1:1}
     *       +2 CONT <ENCODED_MULTIMEDIA_LINE>  {1:M}
     *     +1 OBJE @<XREF:OBJE>@     // chain to continued object   {0:1}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     * 
     * Large whole multimedia objects embedded in a GEDCOM file would break some systems. For this purpose, large multimedia files should be divided into smaller multimedia records by using the subordinate OBJE tag to chain to the next <MULTIMEDIA_RECORD> fragment. This will allow GEDCOM records to be maintained below the 32K limit for use in systems with limited resources. 
     * 
     */ 

    public partial class MultimediaRecord : Record
    {
        private MultimediaFormat format;
        private string title;
        private List<NoteStructure> notes;
        private Blob_ blob;
        private string continuedObjectXRef;
        private List<UserReference> userReferences;
        private string automatedRecordId;
        private ChangeDate changeDate;

        public MultimediaRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            notes = new List<NoteStructure>();
            userReferences = new List<UserReference>();

            Tag = "OBJE";

            format = MultimediaFormat.Unknown;
        }

        [Tag("FORM", MultimediaFormat.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public MultimediaFormat Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }

        [Tag("TITL")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,248)]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
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

        [Tag("BLOB")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public Blob_ Blob
        {
            get
            {
                return blob;
            }
            set
            {
                blob = value;
            }
        }

        [Tag("OBJE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string ContinuedObjectXRef
        {
            get
            {
                return continuedObjectXRef;
            }
            set
            {
                continuedObjectXRef = value;
            }
        }

        [Tag("REFN", typeof(UserReference))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<UserReference> UserReferences
        {
            get
            {
                return userReferences;
            }
            set
            {
                userReferences = value;
            }
        }

        [Tag("RIN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AutomatedRecordId
        {
            get
            {
                return automatedRecordId;
            }
            set
            {
                automatedRecordId = value;
            }
        }

        [Tag("CHAN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ChangeDate ChangeDate
        {
            get
            {
                return changeDate;
            }
            set
            {
                changeDate = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            MultimediaRecord mr = obj as MultimediaRecord;
            if (mr == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Format, mr.Format))
            {
                return false;
            }

            if (!CompareObjects(Title, mr.Title))
            {
                return false;
            }

            if (!Notes.Count.Equals(mr.Notes.Count))
            {
                return false;
            }

            if (!CompareObjects(Blob, mr.Blob))
            {
                return false;
            }

            if (!CompareObjects(ContinuedObjectXRef, mr.ContinuedObjectXRef))
            {
                return false;
            }

            if (!UserReferences.Count.Equals(mr.UserReferences.Count))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, mr.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, mr.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
