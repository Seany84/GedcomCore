using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public class ContinueableText : GedcomLine
    {
        private string text;

        private List<AdditionalLine> tmp;

        public ContinueableText(Reporting Reporting) 
            : this("", Reporting)
        {
        }

        public ContinueableText(string Text, Reporting Reporting)
            : base(Reporting)
        {
            text = Text;
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string Text
        {
            get
            {
                string[] s = text.Split(new string[] { "\n" }, StringSplitOptions.None);
                tmp = new List<AdditionalLine>();
                for (int n = 0; n < s.Length - 1; n++)
                {
                    // a line (including spaces, the tag, etc) must not be longer than 255 characters
                    // it should be safe to assume that a content-part of 240 is ok
                    string[] parts = MakeParts(s[n + 1], 240);

                    AdditionalLine contLine = new AdditionalLine(parts[0], Reporting);
                    contLine.Tag = "CONT";
                    tmp.Add(contLine);

                    for(int m=1; m<parts.Length; m++)
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
                text = value;
            }
        }

        public static string[] MakeParts(string Str, int MaxLength)
        {
            string[] parts = new string[(Str.Length / MaxLength)+1];
            for (int n = 0; n < parts.Length; n++)
            {
                int remainingLength = Str.Length - MaxLength * n;
                parts[n] = Str.Substring(MaxLength * n, remainingLength > MaxLength ? MaxLength : remainingLength);
            }
            return parts;
        }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Continue
        {
            set
            {
                text += "\n" + value;
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

        public IEnumerable<string> AllLines
        {
            get
            {
                yield return Text;
                foreach (AdditionalLine l in AdditionalLines)
                {
                    yield return l.String;
                }
            }
        }

        [Tag("CONC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Concatenate
        {
            set
            {
                text += value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            ContinueableText txt = obj as ContinueableText;
            if (txt == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Text, txt.Text))
            {
                return false;
            }

            return true;
        }
    }
}
