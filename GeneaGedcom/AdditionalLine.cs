using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public class AdditionalLine : GedcomLine
    {
        public AdditionalLine(Reporting Reporting)
            : base(Reporting)
        {
            String = "";
        }

        public AdditionalLine(string Str, Reporting Reporting)
            : base(Reporting)
        {
            String = Str;
        }

        [Tag("")]
        public string String { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var s = obj as AdditionalLine;
            if (s == null)
            {
                return false;
            }

            return CompareObjects(String, s.String);
        }
    }
}
