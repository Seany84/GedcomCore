using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
        private AdoptionFamily_ adoptionFamily;

        public IndividualEventAdoption(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "ADOP";
        }

        [Tag("FAMC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public AdoptionFamily_ AdoptionFamily
        {
            get => adoptionFamily;
            set => adoptionFamily = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var adoption = obj as IndividualEventAdoption;
            if (adoption == null)
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
