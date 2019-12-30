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
        private Date beforeDate;
        private Date afterDate;
        private Date betweenDate1;
        private Date betweenDate2;

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
            get
            {
                return "";
            }
            set
            {
                value = value.Trim();

                int beforeIndex = value.IndexOf(before);
                int afterIndex = value.IndexOf(after);
                int betweenIndex = value.IndexOf(between);
                int andIndex = value.IndexOf(and);

                bool isBefore = beforeIndex != -1;
                bool isAfter = afterIndex != -1;
                bool isBetween = (betweenIndex != -1) && (andIndex != -1);

                if (!isBefore && !isAfter && !isBetween)
                {
                    throw new FormatException("no daterange: " + DateString);
                }


                if (isBefore)
                {
                    string str = value.Substring(beforeIndex + before.Length);
                    BeforeDate = new Date(str, Reporting);
                }
                else if (isAfter)
                {
                    string str = value.Substring(afterIndex + after.Length);
                    AfterDate = new Date(str, Reporting);
                }
                else if (isBetween)
                {
                    string str1 = value.Substring(betweenIndex + before.Length, andIndex - (betweenIndex + before.Length));
                    string str2 = value.Substring(andIndex + and.Length);
                    BetweenDate1 = new Date(str1, Reporting);
                    betweenDate2 = new Date(str2, Reporting);
                }
            }
        }

        public Date BeforeDate
        {
            get
            {
                return beforeDate;
            }
            set
            {
                beforeDate = value;
            }
        }

        public Date AfterDate
        {
            get
            {
                return afterDate;
            }
            set
            {
                afterDate = value;
            }
        }

        public Date BetweenDate1
        {
            get
            {
                return betweenDate1;
            }
            set
            {
                betweenDate1 = value;
            }
        }

        public Date BetweenDate2
        {
            get
            {
                return betweenDate2;
            }
            set
            {
                betweenDate2 = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            DateRange dr = obj as DateRange;
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
