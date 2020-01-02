using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Individual
{
    /* 
     *   n[ BIRT | CHR ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {0:1}
     * 
     */

    public class IndividualEventBirth : IndividualEvent
    {
        public IndividualEventBirth(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string FamilyXref { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is IndividualEventBirth birth))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(FamilyXref, birth.FamilyXref))
            {
                return false;
            }

            return true;
        }
    }
}
