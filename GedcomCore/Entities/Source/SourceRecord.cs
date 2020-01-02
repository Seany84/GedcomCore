using System;
using System.Collections.Generic;
using GedcomCore.Framework.Entities.Date;
using GedcomCore.Framework.Entities.Multimedia;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Source
{
    /* 
     * SOURCE_RECORD: =
     * 
     *   n  @<XREF:SOUR>@ SOUR  {1:1}
     *     +1 DATA        {0:1}
     *       +2 EVEN <EVENTS_RECORDED>  {0:M}
     *         +3 DATE <DATE_PERIOD>  {0:1}
     *         +3 PLAC <SOURCE_JURISDICTION_PLACE>  {0:1}
     *       +2 AGNC <RESPONSIBLE_AGENCY>  {0:1}
     *       +2 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 AUTH <SOURCE_ORIGINATOR>  {0:1}
     *       +2 [CONT|CONC] <SOURCE_ORIGINATOR>  {0:M}
     *     +1 TITL <SOURCE_DESCRIPTIVE_TITLE>  {0:1}
     *       +2 [CONT|CONC] <SOURCE_DESCRIPTIVE_TITLE>  {0:M}
     *     +1 ABBR <SOURCE_FILED_BY_ENTRY>  {0:1}
     *     +1 PUBL <SOURCE_PUBLICATION_FACTS>  {0:1}
     *       +2 [CONT|CONC] <SOURCE_PUBLICATION_FACTS>  {0:M}
     *     +1 TEXT <TEXT_FROM_SOURCE>  {0:1}
     *       +2 [CONT|CONC] <TEXT_FROM_SOURCE>  {0:M}
     *     +1 <<SOURCE_REPOSITORY_CITATION>>  {0:1}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     * 
     * Source records are used to provide a bibliographic description of the source cited. (See the <<SOURCE_CITATION>> structure, which contains the pointer to this source record.) 
     * 
     */
 
    public partial class SourceRecord : Record
    {
        public SourceRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            Multimedia = new List<MultimediaLink>();
            Notes = new List<NoteStructure>();
            UserReferences = new List<UserReference>();

            Tag = "SOUR";
        }

        [Tag("DATA")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Data_ Data { get; set; }

        [Tag("AUTH")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText Authority { get; set; }

        [Tag("TITL")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText Title { get; set; }

        [Tag("ABBR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,60)]
        public string Abbreviation { get; set; }

        [Tag("PUBL")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText PublicationFacts { get; set; }

        [Tag("TEXT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText Text { get; set; }

        [Tag("REPO")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public SourceRepositoryCitation SourceRepositoryCitation { get; set; }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        [Tag("REFN", typeof(UserReference))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        [Length(1,20)]
        public List<UserReference> UserReferences { get; set; }

        [Tag("RIN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AutomatedRecordId { get; set; }

        [Tag("CHAN", typeof(ChangeDate))]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ChangeDate ChangeDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            if (!(obj is SourceRecord sr))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Data, sr.Data))
            {
                return false;
            }

            if (!CompareObjects(Authority, sr.Authority))
            {
                return false;
            }

            if (!CompareObjects(Title, sr.Title))
            {
                return false;
            }

            if (!CompareObjects(Abbreviation, sr.Abbreviation))
            {
                return false;
            }

            if (!CompareObjects(PublicationFacts, sr.PublicationFacts))
            {
                return false;
            }

            if (!CompareObjects(Text, sr.Text))
            {
                return false;
            }

            if (!CompareObjects(SourceRepositoryCitation, sr.SourceRepositoryCitation))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(sr.Multimedia.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(sr.Notes.Count))
            {
                return false;
            }

            if (!UserReferences.Count.Equals(sr.UserReferences.Count))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, sr.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, sr.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
