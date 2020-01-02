using System;
using System.Collections.Generic;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    public abstract class GedcomLine
    {
        private readonly List<KeyValuePair<string, GedcomLine>> customTags;

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

        public override bool Equals(object Obj)
        {
            if (Obj == null)
            {
                throw new ArgumentNullException();
            }

            return Obj is GedcomLine line && CompareObjects(Tag, line.Tag);
        }

        protected bool Equals(GedcomLine Other)
        {
            return Equals(customTags, Other.customTags) && Tag == Other.Tag && Equals(Reporting, Other.Reporting);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(customTags, Tag, Reporting);
        }

        protected Reporting Reporting { get; }
    }
}
