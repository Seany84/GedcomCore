using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_RANGE: = {Size=8:35}
     * [
     * BEF <DATE> |
     * AFT <DATE> |
     * BET <DATE> AND <DATE>
     * ]
     * 
     * Where :
     * AFT =Event happened after the given date.
     * BEF =Event happened before the given date.
     * BET =Event happened some time between date 1 AND date 2. For example, bet 1904 and 1915 indicates that the event state (perhaps a single day) existed somewhere between 1904 and 1915 inclusive.
     * 
     * The date range differs from the date period in that the date range is an estimate that an event happened on a single date somewhere in the date range specified.
     * 
     * The following are equivalent and interchangeable:
     * 
     *   Short form  Long Form
     *   ----------  ---------
     *   1852        BET 1 JAN 1852 AND 31 DEC 1852
     *   1852        BET 1 JAN 1852 AND DEC 1852
     *   1852        BET JAN 1852 AND 31 DEC 1852
     *   1852        BET JAN 1852 AND DEC 1852
     *   JAN 1920    BET 1 JAN 1920 AND 31 JAN 1920
     * 
     */
 
    public class DateRange : DateValue
    {
        private const string before = "BEF ";
        private const string after = "AFT ";
        private const string between = "BET ";
        private const string and = " AND ";

        public DateRange(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override string DateString
        {
            get => "";
            set
            {
                value = value.Trim();

                var beforeIndex = value.IndexOf(before);
                var afterIndex = value.IndexOf(after);
                var betweenIndex = value.IndexOf(between);
                var andIndex = value.IndexOf(and);

                var isBefore = beforeIndex != -1;
                var isAfter = afterIndex != -1;
                var isBetween = (betweenIndex != -1) && (andIndex != -1);

                if (!isBefore && !isAfter && !isBetween)
                {
                    throw new FormatException("no daterange: " + DateString);
                }


                if (isBefore)
                {
                    var str = value.Substring(beforeIndex + before.Length);
                    BeforeDate = new Date(str, Reporting);
                }
                else if (isAfter)
                {
                    var str = value.Substring(afterIndex + after.Length);
                    AfterDate = new Date(str, Reporting);
                }
                else if (isBetween)
                {
                    var str1 = value.Substring(betweenIndex + before.Length, andIndex - (betweenIndex + before.Length));
                    var str2 = value.Substring(andIndex + and.Length);
                    BetweenDate1 = new Date(str1, Reporting);
                    BetweenDate2 = new Date(str2, Reporting);
                }
            }
        }

        public Date BeforeDate { get; set; }

        public Date AfterDate { get; set; }

        public Date BetweenDate1 { get; set; }

        public Date BetweenDate2 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dr = obj as DateRange;
            if (dr == null)
            {
                return false;
            }

            if (!CompareObjects(BeforeDate, dr.BeforeDate))
            {
                return false;
            }

            if (!CompareObjects(AfterDate, dr.AfterDate))
            {
                return false;
            }

            if (!CompareObjects(BetweenDate1, dr.BetweenDate1))
            {
                return false;
            }

            if (!CompareObjects(BetweenDate2, dr.BetweenDate2))
            {
                return false;
            }

            return true;
        }
    }
}
