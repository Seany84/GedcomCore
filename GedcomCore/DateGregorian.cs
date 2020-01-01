using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    /*
     * DATE_GREG: = {Size=4:35}
     * [ <YEAR_GREG> | <MONTH> <YEAR_GREG> | <DAY> <MONTH> <YEAR_GREG> ]
     * See <YEAR_GREG>. 
     * 
     */

    public class DateGregorian : DateCalendar
    {
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
            this.Day = Day;
            this.Month = Month;
            this.Year = Year;
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
                            return $"{Day} {EnumTagUtil.GetFirstTagName(Month)} {Year}";
                        }
                        else
                        {
                            return $"{EnumTagUtil.GetFirstTagName(Month)} {Year}";
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
                    Day = int.Parse(words[dayIndex]);
                }

                if (monthIndex >= 0)
                {
                    try
                    {
                        Month = (Month)EnumTagUtil.SelectMember(typeof(Month), words[monthIndex], Month);
                    }
                    catch
                    {
                        throw new FormatException("unknown gregorian month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    Year = new YearGregorian(words[yearIndex]);
                }
            }
        }

        public int Day { get; set; }

        public Month Month { get; set; }

        public YearGregorian Year { get; set; }

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
