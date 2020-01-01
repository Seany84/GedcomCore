using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /*
     *   n  @<XREF:SUBN>@ SUBN  {1:1]
     *     +1 SUBM @<XREF:SUBM>@ {0:1}
     *     +1 FAMF <NAME_OF_FAMILY_FILE>  {0:1}
     *     +1 TEMP <TEMPLE_CODE>  {0:1}
     *     +1 ANCE <GENERATIONS_OF_ANCESTORS>  {0:1}
     *     +1 DESC <GENERATIONS_OF_DESCENDANTS>  {0:1}
     *     +1 ORDI <ORDINANCE_PROCESS_FLAG>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     * 
     */

    public class SubmissionRecord : Record
    {
        public SubmissionRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            Tag = "SUBN";
            OrdianceProcessFlag = OrdinanceProcessFlag.Unknown;
            GenerationsOfAncestors = -1;
            GenerationsOfDescandants = -1;
        }

        [Tag("SUBM")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SubmitterXRef { get; set; }

        [Tag("FAMF")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string FamilyFileName { get; set; }

        [Tag("TEMP")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(4,5)]
        public string TempleCode { get; set; }

        [Tag("ANCE", -1)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public int GenerationsOfAncestors { get; set; }

        [Tag("DESC", -1)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public int GenerationsOfDescandants { get; set; }

        [Tag("ORDI", OrdinanceProcessFlag.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public OrdinanceProcessFlag OrdianceProcessFlag { get; set; }

        [Tag("RIN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AutomatedRecordId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var subm = obj as SubmissionRecord;
            if (subm == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(SubmitterXRef, subm.SubmitterXRef))
            {
                return false;
            }

            if (!CompareObjects(FamilyFileName, subm.FamilyFileName))
            {
                return false;
            }

            if (!CompareObjects(TempleCode, subm.TempleCode))
            {
                return false;
            }

            if (!CompareObjects(GenerationsOfAncestors, subm.GenerationsOfAncestors))
            {
                return false;
            }

            if (!CompareObjects(GenerationsOfDescandants, subm.GenerationsOfDescandants))
            {
                return false;
            }

            if (!CompareObjects(OrdianceProcessFlag, subm.OrdianceProcessFlag))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, subm.AutomatedRecordId))
            {
                return false;
            }

            return true;
        }
    }
}
