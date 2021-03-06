using System;
using GedcomCore.Framework.Entities.Event;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Family
{
    /*
     * FAMILY_EVENT_STRUCTURE: =
     * 
     *   [
     *   n [ ANUL | CENS | DIV | DIVF ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n [ ENGA | MARR | MARB | MARC ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n [ MARL | MARS ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  EVEN          {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   ]
     * 
     */

    public class FamilyEvent : EventDetail
    {
        public FamilyEvent(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(0,1)]
        public string Happened { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is FamilyEvent e))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            return CompareObjects(Happened, e.Happened);
        }
    }
}
