using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * INDIVIDUAL_RECORD: =
     * 
     * n @<XREF:INDI>@  INDI {1:1}
     *     +1 RESN <RESTRICTION_NOTICE>  {0:1}
     *     +1 <<PERSONAL_NAME_STRUCTURE>>  {0:M}
     *     +1 SEX <SEX_VALUE>   {0:1}
     *     +1 <<INDIVIDUAL_EVENT_STRUCTURE>>  {0:M}
     *     +1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>>  {0:M}
     *     +1 <<LDS_INDIVIDUAL_ORDINANCE>>  {0:M}
     *     +1 <<CHILD_TO_FAMILY_LINK>>  {0:M}
     *     +1 <<SPOUSE_TO_FAMILY_LINK>>  {0:M}
     *     +1 SUBM @<XREF:SUBM>@  {0:M}
     *     +1 <<ASSOCIATION_STRUCTURE>>  {0:M}
     *     +1 ALIA @<XREF:INDI>@  {0:M}
     *     +1 ANCI @<XREF:SUBM>@  {0:M}
     *     +1 DESI @<XREF:SUBM>@  {0:M}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 RFN <PERMANENT_RECORD_FILE_NUMBER>  {0:1}
     *     +1 AFN <ANCESTRAL_FILE_NUMBER>  {0:1}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     * 
     * The individual record is a compilation of facts, known or discovered, about an individual. Sometimes these facts are from different sources. This form allows documentation of the source where each of the facts were discovered.
     * 
     * The normal lineage links are shown through the use of pointers from the individual to a family through either the FAMC tag or the FAMS tag. The FAMC tag provides a pointer to a family where this person is a child. The FAMS tag provides a pointer to a family where this person is a spouse or parent. The <<CHILD_TO_FAMILY_LINK>> structure contains a FAMC pointer which is required to show any child to parent linkage for pedigree navigation. The <<CHILD_TO_FAMILY_LINK>> structure also indicates whether the pedigree link represents a birth lineage, an adoption lineage, or a sealing lineage.
     * 
     * Linkage between a child and the family they belonged to at the time of an event can also optionally be shown by a FAMC pointer subordinate to the appropriate event. For example, a FAMC pointer subordinate to an adoption event would show which family adopted this individual. Biological parent or parents can be indicated by a FAMC pointer subordinate to thebirth event. The FAMC tag can also optionally be used subordinate to an ADOPtion, or BIRTh event to differentiate which set of parents were related by adoption, sealing, or birth.
     * 
     * Other associations or relationships are represented by the ASSOciation tag. The person's relation or association is the person being pointed to . The association or relationship is stated by the value on the subordinate RELA line. For example:
     * 
     *       0 @I1@ INDI
     *         1 NAME Fred/Jones/
     *         1 ASSO @I2@
     *           2 RELA Godfather
     * 
     */
 
    public class IndividualRecord : Record
    {
        public IndividualRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            PersonalNames = new List<PersonalName>();
            IndividualEvents = new List<IndividualEvent>();
            Attributes = new List<IndividualAttribute>();
            IndividualOrdiances = new List<LdsIndividualOrdiance>();
            ChildToFamilyLinks = new List<ChildToFamilyLink>();
            SpouseToFamilyLinks = new List<SpouseToFamilyLink>();
            SubmitterXRefs = new List<string>();
            Associations = new List<Association>();
            AliasXRefs = new List<string>();
            AncestorsInterestXRefs = new List<string>();
            DescandantsInterestXRefs = new List<string>();
            SourceCitations = new List<SourceCitation>();
            Multimedia = new List<MultimediaLink>();
            Notes = new List<NoteStructure>();
            UserReferences = new List<UserReference>();

            Sex = Sex.Unknown;
            RestrictionNotice = RestrictionNotice.Unknown;
        }

        [Tag("RESN", RestrictionNotice.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public RestrictionNotice RestrictionNotice { get; set; }

        [Tag("NAME", typeof(PersonalName))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<PersonalName> PersonalNames { get; set; }

        [Tag("SEX", Sex.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Sex Sex { get; set; }

        [Tag("BIRT", typeof(IndividualEventBirth))]
        [Tag("CHR", typeof(IndividualEventBirth))]
        [Tag("DEAT", typeof(IndividualEvent))]
        [Tag("BURI", typeof(IndividualEvent))]
        [Tag("CREM", typeof(IndividualEvent))]
        [Tag("ADOP", typeof(IndividualEventAdoption))]
        [Tag("BAPM", typeof(IndividualEvent))]
        [Tag("BARM", typeof(IndividualEvent))]
        [Tag("BASM", typeof(IndividualEvent))]
        [Tag("BLES", typeof(IndividualEvent))]
        [Tag("CHRA", typeof(IndividualEvent))]
        [Tag("CONF", typeof(IndividualEvent))]
        [Tag("FCOM", typeof(IndividualEvent))]
        [Tag("ORDN", typeof(IndividualEvent))]
        [Tag("NATU", typeof(IndividualEvent))]
        [Tag("EMIG", typeof(IndividualEvent))]
        [Tag("IMMI", typeof(IndividualEvent))]
        [Tag("CENS", typeof(IndividualEvent))]
        [Tag("PROB", typeof(IndividualEvent))]
        [Tag("WILL", typeof(IndividualEvent))]
        [Tag("GRAD", typeof(IndividualEvent))]
        [Tag("RETI", typeof(IndividualEvent))]
        [Tag("EVEN", typeof(IndividualEvent))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<IndividualEvent> IndividualEvents { get; set; }


        [Tag("CAST", typeof(IndividualAttribute))]
        [Tag("DSCR", typeof(IndividualAttribute))]
        [Tag("EDUC", typeof(IndividualAttribute))]
        [Tag("IDNO", typeof(IndividualAttribute))]
        [Tag("NATI", typeof(IndividualAttribute))]
        [Tag("NCHI", typeof(IndividualAttribute))]
        [Tag("NMR", typeof(IndividualAttribute))]
        [Tag("OCCU", typeof(IndividualAttribute))]
        [Tag("PROP", typeof(IndividualAttribute))]
        [Tag("RELI", typeof(IndividualAttribute))]
        [Tag("RESI", typeof(IndividualAttribute))]
        [Tag("SSN", typeof(IndividualAttribute))]
        [Tag("TITL", typeof(IndividualAttribute))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<IndividualAttribute> Attributes { get; set; }

        [Tag("BAPL", typeof(LdsIndividualOrdianceBaptism))]
        [Tag("CONL", typeof(LdsIndividualOrdianceBaptism))]
        [Tag("ENDL", typeof(LdsIndividualOrdianceEndowment))]
        [Tag("SLGC", typeof(LdsIndividualOrdianceChildSealing))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<LdsIndividualOrdiance> IndividualOrdiances { get; set; }

        [Tag("FAMC", typeof(ChildToFamilyLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<ChildToFamilyLink> ChildToFamilyLinks { get; set; }

        [Tag("FAMS", typeof(SpouseToFamilyLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SpouseToFamilyLink> SpouseToFamilyLinks { get; set; }

        [Tag("SUBM", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> SubmitterXRefs { get; set; }

        [Tag("ASSO", typeof(Association))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<Association> Associations { get; set; }

        /// <summary>
        /// links to different records who may be the same person. 
        /// </summary>
        [Tag("ALIA", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> AliasXRefs { get; set; }

        /// <summary>
        /// Indicates an interest in additional research for ancestors of this individual.
        /// </summary>
        [Tag("ANCI", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> AncestorsInterestXRefs { get; set; }

        /// <summary>
        /// Indicates an interest in research to identify additional descendants of this individual.
        /// </summary>
        [Tag("DESI", typeof(string))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<string> DescandantsInterestXRefs { get; set; }

        /// <summary>
        /// The initial or original material from which information was obtained. 
        /// </summary>
        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        [Tag("RFN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string PermanentRecordFileNumber { get; set; }

        [Tag("AFN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AncestralFileNumber { get; set; }

        [Tag("REFN", typeof(UserReference))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
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

            var indi = obj as IndividualRecord;
            if (indi == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(RestrictionNotice, indi.RestrictionNotice))
            {
                return false;
            }

            if (!PersonalNames.Count.Equals(indi.PersonalNames.Count))
            {
                return false;
            }

            if (!CompareObjects(Sex, indi.Sex))
            {
                return false;
            }

            if (!IndividualEvents.Count.Equals(indi.IndividualEvents.Count))
            {
                return false;
            }

            if (!Attributes.Count.Equals(indi.Attributes.Count))
            {
                return false;
            }

            if (!IndividualOrdiances.Count.Equals(indi.IndividualOrdiances.Count))
            {
                return false;
            }

            if (!ChildToFamilyLinks.Count.Equals(indi.ChildToFamilyLinks.Count))
            {
                return false;
            }

            if (!SpouseToFamilyLinks.Count.Equals(indi.SpouseToFamilyLinks.Count))
            {
                return false;
            }

            if (!SubmitterXRefs.Count.Equals(indi.SubmitterXRefs.Count))
            {
                return false;
            }

            if (!Associations.Count.Equals(indi.Associations.Count))
            {
                return false;
            }

            if (!AliasXRefs.Count.Equals(indi.AliasXRefs.Count))
            {
                return false;
            }

            if (!AncestorsInterestXRefs.Count.Equals(indi.AncestorsInterestXRefs.Count))
            {
                return false;
            }

            if (!DescandantsInterestXRefs.Count.Equals(indi.DescandantsInterestXRefs.Count))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(indi.SourceCitations.Count))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(indi.Multimedia.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(indi.Notes.Count))
            {
                return false;
            }

            if (!CompareObjects(PermanentRecordFileNumber, indi.PermanentRecordFileNumber))
            {
                return false;
            }

            if (!CompareObjects(AncestralFileNumber, indi.AncestralFileNumber))
            {
                return false;
            }

            if (!CompareObjects(UserReferences, indi.UserReferences))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, indi.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, indi.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
