using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities
{
    /* 
     * SEX_VALUE: = {Size=1:7}
     * A code that indicates the sex of the individual:
     * M = Male
     * F = Female 
     * 
     */
 
    [Length(1,7)]
    public enum Sex
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("M")]
        Male,

        [EnumTag("F")]
        Female
    }
}
