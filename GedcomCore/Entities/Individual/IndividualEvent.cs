using System;
using GedcomCore.Framework.Entities.Event;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Individual
{
    /* 
     * INDIVIDUAL_EVENT_STRUCTURE: =
     * 
     *   [
     *   n[ BIRT | CHR ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {0:1}
     *     |
     *   n  [ DEAT | BURI | CREM ] [Y|<NULL>]   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  ADOP [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     +1 FAMC @<XREF:FAM>@  {0:1}
     *       +2 ADOP <ADOPTED_BY_WHICH_PARENT>  {0:1}
     *     |
     *   n  [ BAPM | BARM | BASM | BLES ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  [ CHRA | CONF | FCOM | ORDN ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  [ NATU | EMIG | IMMI ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  [ CENS | PROB | WILL] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  [ GRAD | RETI ] [Y|<NULL>]  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     |
     *   n  EVEN          {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *     ]
     * 
     * 
     * The EVEN tag in this structure is for recording general events or attributes that are not shown in the above <<INDIVIDUAL_EVENT_STRUCTURE>>. The general event or attribute type is declared by using a subordinate TYPE tag to show what event or attribute is recorded. For example, a candidate for state senate in the 1952 election could be recorded:
     * 
     *     1 EVEN
     *       2 TYPE Election
     *       2 DATE 07 NOV 1952
     *       2 NOTE Candidate for State Senate.
     * 
     * 
     * The TYPE tag is also optionally used to modify the basic understanding of its superior event and is usually provided by the user. For example:
     * 
     *     1 ORDN
     *       2 TYPE Deacon
     * 
     * 
     * The presence of a DATE tag and/or PLACe tag makes the assertion of when and/or where the event took place, and therefore that the event did happen. The absence of both of these tags require a Y(es) value on the parent TAG line to assert that the event happened. Using this convention protects GEDCOM processors which may remove (prune) lines that have no value and no subordinate lines. It also allows a note or source to be attached to the event context without implying that the event occurred.
     * 
     * It is not proper to use a N(o) value with an event tag to infer that it did not happen. Inferring that an event did not occur would require a different tag. A symbol such as using an exclamation mark (!) preceding an event tag to indicate an event is known not to have happened may be defined in the future. 
     * 
     */
 
    public class IndividualEvent : EventDetail
    {
        private EventTypeIndividual type;

        public IndividualEvent(Reporting Reporting)
            : base(Reporting)
        {
            type = EventTypeIndividual.Unknown;
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(0,1)]
        public string Happened { get; set; }

        public EventTypeIndividual TypeEnum
        {
            get => type;
            set => Tag = EnumTagUtil.GetFirstTagName(type);
        }

        public override string Tag
        {
            get => base.Tag;
            set
            {
                base.Tag = value;

                type = (EventTypeIndividual)EnumTagUtil.SelectMember(typeof(EventTypeIndividual), value, type);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var e = obj as IndividualEvent;
            if (e == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Happened, e.Happened))
            {
                return false;
            }

            if (!CompareObjects(TypeEnum, e.TypeEnum))
            {
                return false;
            }

            return true;
        }
    }
}
