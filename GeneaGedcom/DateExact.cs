using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /*
     * DATE_EXACT: = {Size=10:11}
     * <DAY> <MONTH> <YEAR_GREG>
     * 
     */
 
    public class DateExact : GedcomLine
    {
        private int day;
        private Month month;
        private YearGregorian year;

        public DateExact(Reporting Reporting)
            : base(Reporting)
        {
        }

        public DateExact(int Day, Month Month, YearGregorian Year, Reporting Reporting)
            : base(Reporting)
        {
            day = Day;
            month = Month;
            year = Year;
        }

        public static DateExact Parse(string DateString, Reporting Reporting)
        {
            var date = new DateExact(Reporting);
            date.DateString = DateString;
            return date;
        }

        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }

        public Month Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }

        public YearGregorian Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string DateString
        {
            get
            {
                string str;

                if (Year.Equals(0))
                {
                    str = "";
                }
                else if (Month == Month.Unknown)
                {
                    str = Year.ToString();
                }
                else if (Day == 0)
                {
                    str = string.Format("{0} {1}", EnumTagUtil.GetFirstTagName(Month));
                }
                else
                {
                    str = string.Format("{0} {1} {2}", Day, EnumTagUtil.GetFirstTagName(Month), Year);
                }

                return str;
            }
            set
            {
                char[] splitChars = { ' ', '\t' };
                var words = value.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 3)
                {
                    Reporting.Warn("exact date doesn't have proper format: " + value);
                }

                if (words.Length == 3)
                {
                    day = int.Parse(words[0]);
                }
                if (words.Length > 1)
                {
                    try
                    {
                        month = (Month)EnumTagUtil.SelectMember(typeof(Month), words[1], month);
                    }
                    catch
                    {
                        throw new FormatException("unknown gregorian month: " + words[1]);
                        //TODO: should we accept numbers instead of month names here, too?
                    }
                }

                year = new YearGregorian(words[2]);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var de = obj as DateExact;
            if (de == null)
            {
                return false;
            }

            if (!CompareObjects(Day, de.Day))
            {
                return false;
            }

            if (!CompareObjects(Month, de.Month))
            {
                return false;
            }

            if (!CompareObjects(Year, de.Year))
            {
                return false;
            }

            return true;
        }
    }
}
