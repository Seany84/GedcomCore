using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public class AdditionalLine : GedcomLine
    {
        private string str;

        public AdditionalLine(Reporting Reporting)
            : base(Reporting)
        {
            str = "";
        }

        public AdditionalLine(string Str, Reporting Reporting)
            : base(Reporting)
        {
            str = Str;
        }

        [Tag("")]
        public string String
        {
            get
            {
                return str;
            }
            set
            {
                str = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            AdditionalLine s = obj as AdditionalLine;
            if (s == null)
            {
                return false;
            }

            return CompareObjects(String, s.String);
        }
    }
}
