using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Individual
{
    /* 
     *   n  ADOP [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {0:1}
     *       +2 ADOP <ADOPTED_BY_WHICH_PARENT>  {0:1}
     * 
     */

    public partial class IndividualEventAdoption : IndividualEvent
    {
        public IndividualEventAdoption(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "ADOP";
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public AdoptionFamily_ AdoptionFamily { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is IndividualEventAdoption adoption))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(AdoptionFamily, adoption.AdoptionFamily))
            {
                return false;
            }

            return true;
        }
    }
}
