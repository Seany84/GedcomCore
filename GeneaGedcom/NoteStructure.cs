using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using System.Text.RegularExpressions;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * NOTE_STRUCTURE: =
     * 
     *   [
     *   n  NOTE @<XREF:NOTE>@  {1:1}
     *     +1 SOUR @<XREF:SOUR>@  {0:M}
     *   |
     *   n  NOTE [<SUBMITTER_TEXT> | <NULL>]  {1:1}
     *     +1 [ CONC | CONT ] <SUBMITTER_TEXT>  {0:M}
     *     +1 SOUR @<XREF:SOUR>@  {0:M}
     *   ]
     * 
     * 
     * SOURce tags subordinate to the note structure can only contain a pointer to an associated source record. Any non-structured reference to a source from a note structure can be included as part of the note text itself. 
     * 
     */ 

    public class NoteStructure : GedcomLine
    {
        private string noteXRef;
        private string submitterText;
        private bool isInlineNote;

        public NoteStructure(Reporting Reporting)
            : base(Reporting)
        {
            SourceCitations = new List<SourceCitation>();
        }

        public NoteStructure(string LineValue, Reporting Reporting)
            : base(Reporting)
        {
            SourceCitations = new List<SourceCitation>();

            Tag = "NOTE";

            this.LineValue = LineValue;
        }

        [Tag("")]
        //TODO: length
        public string LineValue
        {
            get
            {
                if (isInlineNote)
                {
                    return SubmitterText;
                }
                else
                {
                    return NoteXRef;
                }
            }
            set
            {
                if (value.StartsWith("@"))
                {
                    NoteXRef = value;
                    isInlineNote = false;
                }
                else
                {
                    SubmitterText = value;
                    isInlineNote = true;
                }
            }
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string NoteXRef
        {
            get => isInlineNote ? null : noteXRef;
            set
            {
                isInlineNote = value != null;

                noteXRef = value;
            }
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SubmitterText
        {
            get
            {
                if (submitterText == null)
                {
                    AdditionalLines = new List<AdditionalLine>();
                    return "";
                }

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
            set
            {
                var regex = new Regex(@"^\s*@(\w+)@\s*$");
                var match = regex.Match(value);

                if (match.Success)
                {
                    NoteXRef = match.Groups[1].Value;
                }
                else
                {
                    submitterText = value;
                }
            }
        }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Continue
        {
            set
            {
                if (!isInlineNote)
                {
                    throw new InvalidOperationException("can't continue text of a reference note-strucutre");
                }

                submitterText += "\n" + value;
            }
        }

        [Tag("CONT", typeof(AdditionalLine))]
        [Tag("CONC", typeof(AdditionalLine))]
        [Length(1, 60)]
        public List<AdditionalLine> AdditionalLines { get; private set; }

        [Tag("CONC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Concatenate
        {
            set
            {
                if (!isInlineNote)
                {
                    throw new InvalidOperationException("can't concatenate to the text of a reference note-strucutre");
                }

                submitterText += value;
            }
        }
                

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var ns = obj as NoteStructure;
            if (ns == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(NoteXRef, ns.NoteXRef))
            {
                return false;
            }

            if (!CompareObjects(SubmitterText, ns.SubmitterText))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(ns.SourceCitations.Count))
            {
                return false;
            }

            if (!CompareObjects(isInlineNote, ns.isInlineNote))
            {
                return false;
            }

            return true;
        }
    }
}
