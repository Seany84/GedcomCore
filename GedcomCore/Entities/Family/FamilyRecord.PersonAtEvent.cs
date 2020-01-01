using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Family
{
    public partial class FamilyRecord
    {
        /* 
 *       +2 HUSB      {0:1}
 *         +3 AGE <AGE_AT_EVENT>  {1:1}
 * and
 *       +2 WIFE      {0:1}
 *         +3 AGE <AGE_AT_EVENT>  {1:1}
 * 
 */

        public class PersonAtEvent : GedcomLine
        {
            public PersonAtEvent(Reporting Reporting)
                : base(Reporting)
            {
            }

            [Tag("AGE")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            public AgeAtEvent AgeAtEvent { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new NullReferenceException();
                }

                var pe = obj as PersonAtEvent;
                if (pe == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!CompareObjects(AgeAtEvent, pe.AgeAtEvent))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
