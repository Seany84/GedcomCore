using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
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
        public MultimediaRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            Notes = new List<NoteStructure>();
            UserReferences = new List<UserReference>();

            Tag = "OBJE";

            Format = MultimediaFormat.Unknown;
        }

        [Tag("FORM", MultimediaFormat.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public MultimediaFormat Format { get; set; }

        [Tag("TITL")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,248)]
        public string Title { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        [Tag("BLOB")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public Blob_ Blob { get; set; }

        [Tag("OBJE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string ContinuedObjectXRef { get; set; }

        [Tag("REFN", typeof(UserReference))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<UserReference> UserReferences { get; set; }

        [Tag("RIN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AutomatedRecordId { get; set; }

        [Tag("CHAN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ChangeDate ChangeDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var mr = obj as MultimediaRecord;
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
