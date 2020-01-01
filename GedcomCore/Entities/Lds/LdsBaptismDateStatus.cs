using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Lds
{
    /*
     * LDS_BAPTISM_DATE_STATUS: = {Size=5:10}
     * [ CHILD | CLEARED | COMPLETED | INFANT | PRE-1970 |
     * QUALIFIED | STILLBORN | SUBMITTED | UNCLEARED ]
     * 
     * A code indicating the status of an LDS baptism and confirmation date where:
     * 
     * CHILD =Died before eight years old.
     * CLEARED = Baptism has been cleared for temple ordinance.
     * COMPLETED =Completed but the date is not known.
     * INFANT =Died before less than one year old, baptism not required.
     * QUALIFIED =Ordinance request qualified by authorized criteria.
     * PRE-1970 =Ordinance is likely completed, another ordinance for this person was converted from temple records of work completed before 1970, therefore this ordinance is assumed to be complete until all records are converted.
     * STILLBORN =Stillborn, baptism not required.
     * SUBMITTED =Ordinance was previously submitted.
     * UNCLEARED =Data for clearing ordinance request was insufficient. 
     * 
     */

    public enum LdsBaptismDateStatus
    {
        [UnknownEnum]
        Unknown,

        /// <summary>
        /// Died beforeDate eight years old.
        /// </summary>
        [EnumTag("CHILD")]
        Child,

        /// <summary>
        /// Baptism has been cleared for temple ordinance.
        /// </summary>
        [EnumTag("CLEARED")]
        Cleared,

        /// <summary>
        /// Completed bute the date is not known.
        /// </summary>
        [EnumTag("COMPLETED")]
        Completed,

        /// <summary>
        /// Died beforeDate less than one year old, baptism not required.
        /// </summary>
        [EnumTag("INFANT")]
        Infant,

        /// <summary>
        /// Ordinance is likely completed, another ordinance for this person
        /// was converted before temple records of work completed beforeDate 1970, 
        /// therefore this ordinance is assumed after be complete until all records
        /// are converted
        /// </summary>
        [EnumTag("PRE-1970")]
        Pre1970,

        /// <summary>
        /// Ordinance request qualified by authorized criteria.
        /// </summary>
        [EnumTag("QUALIFIED")]
        Qualified,

        /// <summary>
        /// Stillborn, baptism not required.
        /// </summary>
        [EnumTag("STILLBORN")]
        Stillborn,

        /// <summary>
        /// Ordinance was previously submitted.
        /// </summary>
        [EnumTag("SUBMITTED")]
        Submitted,

        /// <summary>
        /// Data for clearing ordinance request was insufficient.
        /// </summary>
        [EnumTag("UNCLEARED")]
        Uncleared
    }
}
