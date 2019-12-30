using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /*
     * LDS_SPOUSE_SEALING_DATE_STATUS: = {Size=3:10}
     * [ CANCELED | CLEARED | COMPLETED | DNS | DNS/CAN | PRE-1970 |
     * QUALIFIED | SUBMITTED | UNCLEARED ]
     * 
     * CANCELED =Canceled and considered invalid.
     * CLEARED = Sealing has been cleared for temple ordinance.
     * COMPLETED =Completed but the date is not known.
     * DNS =This record is not being submitted for this temple ordinances.
     * DNS/CAN =This record is not being submitted for this temple ordinances.
     * QUALIFIED =Ordinance request qualified by authorized criteria.
     * PRE-1970 = (See pre-1970 under LDS_BAPTISM_DATE_STATUS.)
     * SUBMITTED =Ordinance was previously submitted.
     * UNCLEARED =Data for clearing ordinance request was insufficient. 
     * 
     */

    public enum LdsSpouseSealingDateStatus
    {
        [UnknownEnum]
        Unknown,

        /// <summary>
        /// Canceled and considered invalid.
        /// </summary>
        [EnumTag("CANCELED")]
        Canceled,

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
        /// This record is not being submitted for this temple ordiances.
        /// </summary>
        [EnumTag("DNS")]
        Dns,

        /// <summary>
        /// This record is not being submitted for this temple ordiances.
        /// </summary>
        [EnumTag("DNS/CAN")]
        DnsCan,

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
        /// Ordiance was previously submitted.
        /// </summary>
        [EnumTag("SUBMITTED")]
        Submitted,

        /// <summary>
        /// Data for clearing ordiance request was insufficient.
        /// </summary>
        [EnumTag("UNCLEARED")]
        Uncleared,

        //Not in standard, but found in tortuse files
        [EnumTag("CHILD")]
        [Obsolete]
        Child
    }
}
