using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public abstract class GedcomLine
    {
        private List<KeyValuePair<string, GedcomLine>> customTags;

        protected GedcomLine(Reporting Reporting)
        {
            this.Reporting = Reporting;
            customTags = new List<KeyValuePair<string, GedcomLine>>();
        }

        public virtual string Tag { get; set; }

        public static bool CompareObjects(object Object1, object Object2)
        {
            if (Object1 == Object2)
            {
                return true;
            }

            if ((Object1 == null) || (Object2 == null))
            {
                return false;
            }

            return Object1.Equals(Object2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var line = obj as GedcomLine;
            if (line == null)
            {
                return false;
            }

            return CompareObjects(Tag, line.Tag);
        }

        protected Reporting Reporting { get; }
    }
}
