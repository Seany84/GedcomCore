using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * RECORD: =
     * [
     * n <<FAM_RECORD>> {1:1}
     * |
     * n <<INDIVIDUAL_RECORD>> {1:1}
     * |
     * n <<MULTIMEDIA_RECORD>> {1:M}
     * |
     * n <<NOTE_RECORD>> {1:1}
     * |
     * n <<REPOSITORY_RECORD>> {1:1}
     * |
     * n <<SOURCE_RECORD>> {1:1}
     * |
     * n <<SUBMITTER_RECORD>> {1:1}
     * ] 
     * 
     */
 
    public abstract class Record : GedcomLine
    {
        private string xref;

        protected Record(string XRef, Reporting Reporting)
            : base(Reporting)
        {
            this.XRef = XRef;
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string XRef
        {
            get
            {
                return xref;
            }
            set
            {
                xref = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var rec = obj as Record;
            if (rec == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(XRef, rec.XRef))
            {
                return false;
            }

            return true;
        }
    }
}
