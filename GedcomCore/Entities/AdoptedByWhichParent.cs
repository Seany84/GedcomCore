using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities
{
    /*
     * ADOPTED_BY_WHICH_PARENT: = {Size=1:4}
     * [ HUSB | WIFE | BOTH ]
     * A code which shows which parent in the associated family record adopted this person.
     * 
     */

    /// <summary>
    /// A code which shows which parent in the associated family record adopted this person.
    /// </summary>
    public enum AdoptedByWhichParent
    {
        [UnknownEnum]
        Unknown,

        /// <summary>
        /// The husband in the associated family adopted this person.
        /// </summary>
        [EnumTag("HUSB")]
        Husband,

        /// <summary>
        /// The wife in the associated family adopted this person.
        /// </summary>
        [EnumTag("WIFE")]
        Wife,

        /// <summary>
        /// Both husband and wife adopted this person.
        /// </summary>
        [EnumTag("BOTH")]
        Both
    }
}
