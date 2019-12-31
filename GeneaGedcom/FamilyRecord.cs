using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    /* 
     * FAM_RECORD: =
     * 
     *   n @<XREF:FAM>@   FAM   {1:1}
     *     +1 <<FAMILY_EVENT_STRUCTURE>>  {0:M}
     *       +2 HUSB      {0:1}
     *         +3 AGE <AGE_AT_EVENT>  {1:1}
     *       +2 WIFE      {0:1}
     *         +3 AGE <AGE_AT_EVENT>  {1:1}
     *     +1 HUSB @<XREF:INDI>@  {0:1}
     *     +1 WIFE @<XREF:INDI>@  {0:1}
     *     +1 CHIL @<XREF:INDI>@  {0:M}
     *     +1 NCHI <COUNT_OF_CHILDREN>  {0:1}
     *     +1 SUBM @<XREF:SUBM>@  {0:M}
     *     +1 <<LDS_SPOUSE_SEALING>>  {0:M}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *       +2 <<NOTE_STRUCTURE>>  {0:M}
     *       +2 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     * 
     * The FAMily record is used to record marriages, common law marriages, and family unions caused by two people becoming the parents of a child. There can be no more than one HUSB/father and one WIFE/mother listed in each FAM_RECORD. If, for example, a man participated in more than one family union, then he would appear in more than oneFAM_RECORD. The family record structure assumes that the HUSB/father is male and WIFE/mother is female.
     * 
     * The preferred order of the CHILdren pointers within a FAMily structure is chronological by birth. 
     * 
     */
 
    public partial class FamilyRecord : Record
    {
        public FamilyRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            FamilyEvents = new List<FamilyEvent_>();
            Children = new List<string>();
            SubmitterXRefs = new List<string>();
            SpouseSealings = new List<LdsSpouseSealing>();
            SourceCitations = new List<SourceCitation>();
            Multimedia = new List<MultimediaLink>();
            Notes = new List<NoteStructure>();
            UserReferences = new List<UserReference>();

            Tag = "FAM";

            NumberOfChildren = -1;
        }

        [Tag("ANUL", typeof(FamilyEvent_))]
        [Tag("CENS", typeof(FamilyEvent_))]
        [Tag("DIV", typeof(FamilyEvent_))]
        [Tag("DIVF", typeof(FamilyEvent_))]
        [Tag("ENGA", typeof(FamilyEvent_))]
        [Tag("MARR", typeof(FamilyEvent_))]
        [Tag("MARB", typeof(FamilyEvent_))]
        [Tag("MARC", typeof(FamilyEvent_))]
        [Tag("MARL", typeof(FamilyEvent_))]
        [Tag("MARS", typeof(FamilyEvent_))]
        [Tag("EVEN", typeof(FamilyEvent_))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<FamilyEvent_> FamilyEvents { get; set; }

        [Tag("HUSB")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string HusbandXRef { get; set; }

        [Tag("WIFE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string WifeXRef { get; set; }

        [Tag("CHIL", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> Children { get; set; }

        [Tag("NCHI", -1)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public int NumberOfChildren { get; set; }

        [Tag("SUBM", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> SubmitterXRefs { get; set; }

        [Tag("SLGS", typeof(LdsSpouseSealing))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<LdsSpouseSealing> SpouseSealings { get; set; }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

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

            var fam = obj as FamilyRecord;
            if (fam == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!FamilyEvents.Count.Equals(fam.FamilyEvents.Count))
            {
                return false;
            }

            if (!CompareObjects(HusbandXRef, fam.HusbandXRef))
            {
                return false;
            }

            if (!CompareObjects(WifeXRef, fam.WifeXRef))
            {
                return false;
            }

            if (!Children.Count.Equals(fam.Children.Count))
            {
                return false;
            }

            if (!CompareObjects(NumberOfChildren, fam.NumberOfChildren))
            {
                return false;
            }

            if (!SubmitterXRefs.Count.Equals(fam.SubmitterXRefs.Count))
            {
                return false;
            }

            if (!SpouseSealings.Count.Equals(fam.SpouseSealings.Count))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(fam.SourceCitations.Count))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(fam.Multimedia.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(fam.Notes.Count))
            {
                return false;
            }

            if (!UserReferences.Count.Equals(fam.UserReferences.Count))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, fam.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, fam.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
