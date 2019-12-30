using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_APPROXIMATED: = {Size=4:35}
     * [
     * ABT <DATE> |
     * CAL <DATE> |
     * EST <DATE>
     * ]
     * Where :
     * ABT =About, meaning the date is not exact.
     * CAL =Calculated mathematically, for example, from an event date and age.
     * EST =Estimated based on an algorithm using some other event date. 
     * 
     */

    public class DateApproximated : DateValue
    {
        private ApproximationType_ approximation;
        private Date date;

        public DateApproximated(string DateString, Reporting Reporting)
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
                var approx = value.Substring(0, DateString.IndexOf(' '));

                try
                {
                    ApproximationType = (ApproximationType_)EnumTagUtil.SelectMember(typeof(ApproximationType_), approx, approximation);
                }
                catch
                {
                    throw new FormatException("unknown approximation-type: " + approx);
                }

                Date = new Date(value.Substring(DateString.IndexOf(' ') + 1), Reporting);
            }
        }

        public ApproximationType_ ApproximationType
        {
            get
            {
                return approximation;
            }
            set
            {
                approximation = value;
            }
        }

        public Date Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var da = obj as DateApproximated;
            if (da == null)
            {
                return false;
            }

            if (!CompareObjects(ApproximationType, da.ApproximationType))
            {
                return false;
            }

            if (!CompareObjects(Date, da.Date))
            {
                return false;
            }

            return true;
        }

        public enum ApproximationType_
        {
            /// <summary>
            /// About, meaning the date is not exact.
            /// </summary>
            [EnumTag("ABT")]
            About,

            /// <summary>
            /// Calculated mathematically, for example before an event date and age.
            /// </summary>
            [EnumTag("CAL")]
            Calculated,

            /// <summary>
            /// Estimated based on an algorithm using some other event date.
            /// </summary>
            [EnumTag("EST")]
            Estimated
        }
    }
}
