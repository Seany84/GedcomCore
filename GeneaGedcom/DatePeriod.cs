using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_PERIOD: = {Size=7:35}
     * [
     * FROM <DATE> |
     * TO <DATE> |
     * FROM <DATE> TO <DATE>
     * ]
     * Where:DATE>
     * FROM =Indicates the beginning of a happening or state.
     * TO =Indicates the ending of a happening or state.
     * 
     * Examples:
     * FROM 1904 to 1915
     * =The state of some attribute existed from 1904 to 1915 inclusive.
     * FROM 1904
     * =The state of the attribute began in 1904 but the end date is unknown.
     * TO 1915
     * =The state ended in 1915 but the begin date is unknown. 
     * 
     */

    public class DatePeriod : DateValue
    {
        private Date fromDate;
        private Date toDate;

        private const string from = "FROM ";
        private const string to = "TO ";

        public DatePeriod(string DateString, Reporting Reporting)
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

                var fromIndex = value.IndexOf(from);
                var toIndex = value.IndexOf(to);

                if ((fromIndex == -1) && (toIndex == -1))
                {
                    throw new FormatException("no dateperiod: " + DateString);
                }

                var fromDateString = "";
                var toDateString = "";

                if (fromIndex != -1)
                {
                    if (toIndex != -1)
                    {
                        fromDateString = value.Substring(fromIndex + from.Length, toIndex - to.Length - 3);
                    }
                    else
                    {
                        fromDateString = value.Substring(fromIndex + from.Length);
                    }
                }

                if (toIndex != -1)
                {
                    toDateString = value.Substring(toIndex + to.Length);
                }


                if (toDateString != "")
                {
                    toDate = new Date(toDateString, Reporting);
                }
                if (fromDateString != "")
                {
                    fromDate = new Date(fromDateString, Reporting);
                }
            }
        }

        public Date FromDate
        {
            get => fromDate;
            set => fromDate = value;
        }

        public Date ToDate
        {
            get => toDate;
            set => toDate = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dp = obj as DatePeriod;
            if (dp == null)
            {
                return false;
            }

            if (!CompareObjects(FromDate, dp.FromDate))
            {
                return false;
            }

            if (!CompareObjects(ToDate, dp.ToDate))
            {
                return false;
            }

            return true;
        }
    }
}
