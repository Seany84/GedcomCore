using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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

            Trailer trailer = obj as Trailer;
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
