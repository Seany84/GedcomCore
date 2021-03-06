using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Family
{
    public partial class FamilyRecord
    {
        /* 
         *     +1 <<FAMILY_EVENT_STRUCTURE>>  {0:M}
         *       +2 HUSB      {0:1}
         *         +3 AGE <AGE_AT_EVENT>  {1:1}
         *       +2 WIFE      {0:1}
         *         +3 AGE <AGE_AT_EVENT>  {1:1}
         * 
         */

        public class FamilyEvent_ : FamilyEvent
        {
            public FamilyEvent_(Reporting Reporting)
                : base(Reporting)
            {
            }

            [Tag("HUSB", typeof(PersonAtEvent))]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public PersonAtEvent Husband { get; set; }

            [Tag("WIFE", typeof(PersonAtEvent))]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public PersonAtEvent Wife { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new NullReferenceException();
                }

                if (!(obj is FamilyEvent_ fe))
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!CompareObjects(Husband, fe.Husband))
                {
                    return false;
                }

                if (!CompareObjects(Wife, fe.Wife))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
