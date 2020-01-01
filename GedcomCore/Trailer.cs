using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    /* 
     * 0 TRLR {1:1} 
     * 
     */

    public class Trailer : GedcomLine
    {
        public Trailer(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "TRLR";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var trailer = obj as Trailer;
            if (trailer == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            return true;
        }
    }
}
