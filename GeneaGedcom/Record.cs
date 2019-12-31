using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
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
        protected Record(string XRef, Reporting Reporting)
            : base(Reporting)
        {
            this.XRef = XRef;
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string XRef { get; set; }

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
