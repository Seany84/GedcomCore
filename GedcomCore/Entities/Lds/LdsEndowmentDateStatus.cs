using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Lds
{
    /* 
     * LDS_ENDOWMENT_DATE_STATUS: = {Size=5:10}
     * [ CHILD | CLEARED | COMPLETED | INFANT | PRE-1970 |
     * QUALIFIED | STILLBORN | SUBMITTED | UNCLEARED ]
     * 
     * A code indicating the status of an LDS endowment ordinance where:
     * 
     * CHILD =Died before eight years old.
     * CLEARED = Endowment has been cleared for temple ordinance.
     * COMPLETED =Completed but the date is not known.
     * INFANT =Died before less than one year old, baptism not required.
     * QUALIFIED =Ordinance request qualified by authorized criteria.
     * PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS.)
     * STILLBORN =Stillborn, ordinance not required.
     * SUBMITTED =Ordinance was previously submitted.
     * UNCLEARED =Data for clearing ordinance request was insufficient. 
     * 
     */

    public enum LdsEndowmentDateStatus
    {
        [UnknownEnum]
        Unknown,

        /// <summary>
        /// Died beforeDate eight years old.
        /// </summary>
        [EnumTag("CHILD")]
        Child,

        /// <summary>
        /// Endowment has been cleared for temple ordiance.
        /// </summary>
        [EnumTag("CLEARED")]
        Cleared,

        /// <summary>
        /// Completed but the date is not known.
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
        /// Ordiance request qualified by authorized criteria.
        /// </summary>
        [EnumTag("QUALIFIED")]
        Qualified,

        /// <summary>
        /// Stillborn, ordiance not required.
        /// </summary>
        [EnumTag("STILLBORN")]
        Stillborn,

        /// <summary>
        /// Ordiance was previously submitted.
        /// </summary>
        [EnumTag("SUBMITTED")]
        Submitted,

        /// <summary>
        /// Data for clearing ordiance request was insufficient.
        /// </summary>
        [EnumTag("UNCLEARED")]
        Uncleared
    }
}
