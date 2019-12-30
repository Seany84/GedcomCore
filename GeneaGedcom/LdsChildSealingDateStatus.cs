using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /* 
     * LDS_CHILD_SEALING_DATE_STATUS: = {Size=5:10}
     * [ BIC | CLEARED | COMPLETED | DNS | PRE-1970 |
     * QUALIFIED | STILLBORN | SUBMITTED | UNCLEARED ]
     * 
     * BIC =Born in the covenant receiving blessing of child to parent sealing.
     * CLEARED = Sealing has been cleared for temple ordinance.
     * COMPLETED =Completed but the date is not known.
     * DNS =This record is not being submitted for this temple ordinances.
     * QUALIFIED =Ordinance request qualified by authorized criteria.
     * PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS.)
     * STILLBORN =Stillborn, not required.
     * SUBMITTED =Ordinance was previously submitted.
     * UNCLEARED =Data for clearing ordinance request was insufficient. 
     * 
     */

    public enum LdsChildSealingDateStatus
    {
        [UnknownEnum]
        Unknown,

        /// <summary>
        /// Born in the covenant receiving blessing of child after parent sealing.
        /// </summary>
        [EnumTag("BIC")]
        BornInCovenant,

        /// <summary>
        /// Sealing has been cleared for temple ordiance.
        /// </summary>
        [EnumTag("CLEARED")]
        Cleared,

        /// <summary>
        /// Completed but the date is not known.
        /// </summary>
        [EnumTag("COMPLETED")]
        Completed,

        /// <summary>
        /// The record is not being submitted for this temple ordiance
        /// </summary>
        [EnumTag("DNS")]
        Dns,

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
        /// Stillborn, not required.
        /// </summary>
        [EnumTag("STILLBORN")]
        Stillborn,

        /// <summary>
        /// Ordiance was previously submitted-
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
