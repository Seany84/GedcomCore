using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Date
{
    /*
     * DATE_CALENDAR_ESCAPE: = {Size=4:15}
     * [ @#DHEBREW@ | @#DROMAN@ | @#DFRENCH R@ | @#DGREGORIAN@ | @#DJULIAN@ | @#DUNKNOWN@ ]
     * The date escape determines the date interpretation by signifying which <DATE_CALENDAR> to use. The default calendar is the Gregorian calendar. 
     * 
     */

    public enum Calendars
    {
        [EnumTag("@#DUNKNOWN@")]
        [UnknownEnum]
        Unknown,

        [EnumTag("@#DGREGORIAN@")]
        Gregorian,

        [EnumTag("@#DJULIAN@")]
        Julian,

        [EnumTag("@#DHEBREW@")]
        Hebrew,

        [EnumTag("@#DFRENCHR@")] //TODO: support tag @#DFRENCH R@ (with space)
        French,

        [EnumTag("@#DROMAN@")]
        Roman
    }
}
