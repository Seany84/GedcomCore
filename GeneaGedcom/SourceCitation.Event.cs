using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class SourceCitation
    {
        /* 
 *     +1 EVEN <EVENT_TYPE_CITED_FROM>  {0:1}
 *       +2 ROLE <ROLE_IN_EVENT>  {0:1}
 * 
 */

        public class Event_ : GedcomLine
        {
            public Event_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "EVEN";
                EventTypeCitedFrom = EventAttributeType.Unknown;
            }

            [Tag("")]
            public string LineValue
            {
                get => EnumTagUtil.GetFirstTagName(EventTypeCitedFrom);
                set
                {
                    try
                    {

                        EventTypeCitedFrom = (EventAttributeType)EnumTagUtil.SelectMember(typeof(EventAttributeType), value, null);
                    }
                    catch
                    {
                        EventTypeCitedFrom = EventAttributeType.Event;
                    }
                }
            }

            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public EventAttributeType EventTypeCitedFrom { get; set; }

            [Tag("ROLE")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public RoleInEvent RoleInEvent { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var e = obj as Event_;
                if (e == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!EventTypeCitedFrom.Equals(e.EventTypeCitedFrom))
                {
                    return false;
                }

                if (!RoleInEvent.Equals(e.RoleInEvent))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
