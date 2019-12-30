using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /*
     * DATE_GREG: = {Size=4:35}
     * [ <YEAR_GREG> | <MONTH> <YEAR_GREG> | <DAY> <MONTH> <YEAR_GREG> ]
     * See <YEAR_GREG>. 
     * 
     */

    public class DateGregorian : DateCalendar
    {
        private int day;
        private Month month;
        private YearGregorian year;

        public DateGregorian(YearGregorian Year, Reporting Reporting)
            : this(Month.Unknown, Year, Reporting)
        {
        }

        public DateGregorian(Month Month, YearGregorian Year, Reporting Reporting)
            : this(0, Month, Year, Reporting)
        {
        }

        public DateGregorian(int Day, Month Month, YearGregorian Year, Reporting Reporting)
            : base(Reporting)
        {
            day = Day;
            month = Month;
            year = Year;
        }

        public DateGregorian(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override Calendars UsedCalendar => Calendars.Gregorian;

        public override string DateString
        {
            get
            {
                if (Year.IsUnknown)
                {
                    return "";
                }
                else
                {
                    if (Month != Month.Unknown)
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
            }
            set
            {
                char[] splitChars = { ' ', '\t' };
                var words = value.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 3)
                {
                    Reporting.Warn("gregorian date doesn't have proper format: " + value);
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
                        month = (Month)EnumTagUtil.SelectMember(typeof(Month), words[monthIndex], month);
                    }
                    catch
                    {
                        throw new FormatException("unknown gregorian month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    year = new YearGregorian(words[yearIndex]);
                }
            }
        }

        public int Day
        {
            get => day;
            set => day = value;
        }

        public Month Month
        {
            get => month;
            set => month = value;
        }

        public YearGregorian Year
        {
            get => year;
            set => year = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dg = obj as DateGregorian;
            if (dg == null)
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Day, dg.Day))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Month, dg.Month))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Year, dg.Year))
            {
                return false;
            }

            return true;
        }
    }
}
