using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Date
{
    /* 
     * MONTH: = {Size=3}
     * [ JAN | FEB | MAR | APR | MAY | JUN |
     * JUL | AUG | SEP | OCT | NOV | DEC ]
     * Where :
     * JAN = January
     * FEB = February
     * MAR = March
     * APR = April
     * MAY = May
     * JUN = June
     * JUL = July
     * AUG = August
     * SEP = September
     * OCT = October
     * NOV = November
     * DEC = December 
     * 
     */

    public enum Month
    {
        [EnumTag("JAN")]
        January = 1,

        [EnumTag("FEB")]
        February = 2,

        [EnumTag("MAR")]
        March = 3,

        [EnumTag("APR")]
        April = 4,

        [EnumTag("MAY")]
        May = 5,

        [EnumTag("JUN")]
        June = 6,

        [EnumTag("JUL")]
        July = 7,

        [EnumTag("AUG")]
        August = 8,

        [EnumTag("SEP")]
        September = 9,

        [EnumTag("OCT")]
        October = 10,

        [EnumTag("NOV")]
        November = 11,

        [EnumTag("DEC")]
        December = 12,

        [UnknownEnum]
        Unknown = 0
    }
}
