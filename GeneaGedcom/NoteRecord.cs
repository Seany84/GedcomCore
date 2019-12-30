using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * NOTE_RECORD: =
     * 
     *   n  @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT>  {1:1}
     *     +1 [ CONC | CONT] <SUBMITTER_TEXT>  {0:M}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     */ 
    
    public class NoteRecord : Record
    {
        private string submitterText;

        public NoteRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            SourceCitations = new List<SourceCitation>();
            UserReferences = new List<UserReference>();

            Tag = "NOTE";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string SubmitterText
        {
            get
            {
                var s = submitterText.Split(new[] { "\n" }, StringSplitOptions.None);
                AdditionalLines = new List<AdditionalLine>();
                for (var n = 0; n < s.Length - 1; n++)
                {
                    // a line (including spaces, the tag, etc) must not be longer than 255 characters
                    // it shoule be safe to assume that a content-part of 240 is ok
                    var parts = ContinueableText.MakeParts(s[n + 1], 240);

                    var contLine = new AdditionalLine(parts[0], Reporting);
                    contLine.Tag = "CONT";
                    AdditionalLines.Add(contLine);

                    for (var m = 1; m < parts.Length; m++)
                    {
                        var concLine = new AdditionalLine(parts[m], Reporting);
                        concLine.Tag = "CONC";
                        AdditionalLines.Add(concLine);
                    }
                }
                return s[0];
            }
            set => submitterText = value;
        }

        [Tag("CONT", typeof(AdditionalLine))]
        [Tag("CONC", typeof(AdditionalLine))]
        [Length(1, 60)]
        public List<AdditionalLine> AdditionalLines { get; private set; }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Continue
        {
            set
            {
                if (submitterText == null)
                {
                    submitterText = "";
                }

                submitterText += "\n" + value;
            }
        }

        [Tag("CONC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Concatenate
        {
            set
            {
                if (submitterText == null)
                {
                    submitterText = "";
                }

                submitterText += value;
            }
        }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

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
                throw new ArgumentNullException();
            }

            var nr = obj as NoteRecord;
            if (nr == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(SubmitterText, nr.SubmitterText))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(nr.SourceCitations.Count))
            {
                return false;
            }

            if (!UserReferences.Count.Equals(nr.UserReferences.Count))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, nr.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, nr.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
