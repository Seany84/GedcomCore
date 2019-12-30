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
        private List<SourceCitation> sourceCitations;
        private List<UserReference> userReferences;
        private string automatedRecordId;
        private ChangeDate changeDate;

        private List<AdditionalLine> tmp;

        public NoteRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            sourceCitations = new List<SourceCitation>();
            userReferences = new List<UserReference>();

            Tag = "NOTE";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string SubmitterText
        {
            get
            {
                string[] s = submitterText.Split(new string[] { "\n" }, StringSplitOptions.None);
                tmp = new List<AdditionalLine>();
                for (int n = 0; n < s.Length - 1; n++)
                {
                    // a line (including spaces, the tag, etc) must not be longer than 255 characters
                    // it shoule be safe to assume that a content-part of 240 is ok
                    string[] parts = ContinueableText.MakeParts(s[n + 1], 240);

                    AdditionalLine contLine = new AdditionalLine(parts[0], Reporting);
                    contLine.Tag = "CONT";
                    tmp.Add(contLine);

                    for (int m = 1; m < parts.Length; m++)
                    {
                        AdditionalLine concLine = new AdditionalLine(parts[m], Reporting);
                        concLine.Tag = "CONC";
                        tmp.Add(concLine);
                    }
                }
                return s[0];
            }
            set
            {
                submitterText = value;
            }
        }

        [Tag("CONT", typeof(AdditionalLine))]
        [Tag("CONC", typeof(AdditionalLine))]
        [Length(1, 60)]
        public List<AdditionalLine> AdditionalLines
        {
            get
            {
                return tmp;
            }
        }

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

        [Tag("CHAN", typeof(ChangeDate))]
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

            NoteRecord nr = obj as NoteRecord;
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