using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * DATE_HEBR: = {Size=4:35}
     * [ <YEAR> | <MONTH_HEBR> <YEAR> | <DAY> <MONTH_HEBR> <YEAR> ]
     * See <MONTH_HEBR>. 
     * 
     */

    public class DateHebrew : DateCalendar
    {
        private int day;
        private MonthHebrew month;
        private int year;

        public DateHebrew(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override Calendars UsedCalendar
        {
            get
            {
                return Calendars.Hebrew;
            }
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

        public MonthHebrew Month
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

        public int Year
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

        public override string DateString
        {
            get
            {
                if (Year != 0)
                {
                    if (Month != MonthHebrew.Unknown)
                    {
                        if (Day != 0)
                        {
                            return string.Format("{0} {1} {2}", Day, EnumTagUtil.GetFirstTagName(Month), Year);
                        }
                        else
                        {
                            return string.Format("{0} {1}", EnumTagUtil.GetFirstTagName(Month), Year);
                        }
                    }
                    else
                    {
                        return Year.ToString();
                    }
                }
                else
                {
                    return "";
                }
            }
            set
            {
                char[] splitChars = { ' ', '\t' };
                var words = value.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 3)
                {
                    Reporting.Warn("hebrew date doesn't have proper format: " + value);
                }

                var dayIndex = words.Length - 3;
                var monthIndex = words.Length - 2;
                var yearIndex = words.Length - 1;

                if (dayIndex >= 0)
                {
                    day = int.Parse(words[dayIndex]);
                }

                if (monthIndex >= 0)
                {
                    try
                    {
                        month = (MonthHebrew)EnumTagUtil.SelectMember(typeof(MonthHebrew), words[monthIndex], month);
                    }
                    catch
                    {
                        throw new FormatException("unknown hebrew month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    year = int.Parse(words[yearIndex]);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dh = obj as DateHebrew;
            if (dh == null)
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Day, dh.Day))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Month, dh.Month))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Year, dh.Year))
            {
                return false;
            }

            return true;
        }
    }
}
