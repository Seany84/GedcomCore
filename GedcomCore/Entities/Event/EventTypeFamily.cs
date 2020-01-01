using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Event
{
    /*
     * EVENT_TYPE_FAMILY: = {Size=3:4}
     * [ ANUL | CENS | DIV | DIVF | ENGA | MARR |
     * MARB | MARC | MARL | MARS | EVEN ]
     * 
     * A code used to indicate the type of family event. The definition is the same as the corresponding event tag defined in Appendix A. 
     * 
     */

    public enum EventTypeFamily
    {
        [EnumTag("ANUL")]
        Annulment,

        [EnumTag("CENS")]
        Census,

        [EnumTag("DIV")]
        Divorce,

        [EnumTag("DIVF")]
        DivorceFiled,

        [EnumTag("ENGA")]
        Engagement,

        [EnumTag("MARR")]
        Marriage,

        [EnumTag("MARB")]
        MarriageBann,

        [EnumTag("MARC")]
        MarriageContract,

        [EnumTag("MARL")]
        MarriageLicense,

        [EnumTag("MARS")]
        MarriageSettlement,

        [EnumTag("EVEN")]
        Event
    }
}
