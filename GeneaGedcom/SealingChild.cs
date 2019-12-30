using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public class SealingChild : IndividualRecord
    {
        public SealingChild(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string FamilyXRef { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var sc = obj as SealingChild;
            if (sc == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(FamilyXRef, sc.FamilyXRef))
            {
                return false;
            }

            return true;
        }
    }
}
