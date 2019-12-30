using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public abstract class GedcomLine
    {
        private List<KeyValuePair<string, GedcomLine>> customTags;

        private string tag;

        private readonly Reporting reporting;

        public GedcomLine(Reporting Reporting)
        {
            reporting = Reporting;
            customTags = new List<KeyValuePair<string, GedcomLine>>();
        }

        public virtual string Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

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

        protected Reporting Reporting
        {
            get
            {
                return reporting;
            }
        }
    }
}
